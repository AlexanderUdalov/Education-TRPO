using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebSocketServer
{
    public class Program
    {
        public static void Main(string[] args) => WebHost.CreateDefaultBuilder(args).UseStartup<Startup>().Build().Run();
    }

    public class Startup
    {
        public static Dictionary<string, string> PreviousValues = new Dictionary<string, string>()
        {
            ["Test"] = "test"
        };
        private static HttpClient _client = new HttpClient();

        public void Configure(IApplicationBuilder app)
        {
            app.UseWebSockets();
            app.Use(async (context, next) =>
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                    await SendMessageAsync(webSocket, PreviousValues.First().Value);
                    await CheckChanges(webSocket);
                }
                else
                    await context.Response.WriteAsync("This server for websocket use!");
            });
        }
        
        private async Task CheckChanges(WebSocket webSocket)
        {
            while (webSocket.State == WebSocketState.Open)
            {
                await SendMessageAsync(webSocket, "Trying get values...");

                CheckCurrentCryptocurrencyAsync(webSocket, "BTC");
                CheckCurrentCryptocurrencyAsync(webSocket, "ETH");

                await Task.Delay(1000);
            }
            await webSocket.CloseAsync(closeStatus: WebSocketCloseStatus.NormalClosure,
                                    statusDescription: "Closed succesfull",
                                    cancellationToken: CancellationToken.None);
        }

        private async void CheckCurrentCryptocurrencyAsync(WebSocket webSocket, string currentCryptocurrency)
        {
            if (!PreviousValues.ContainsKey(currentCryptocurrency))
                PreviousValues[currentCryptocurrency] = currentCryptocurrency;

            var currentValue = await _client.GetStringAsync(@"https://min-api.cryptocompare.com/data/price?fsym=" +
                currentCryptocurrency + "&tsyms=USD");

            if (!currentValue.Equals(PreviousValues[currentCryptocurrency]))
            {
                PreviousValues[currentCryptocurrency] = currentValue;
                await SendMessageAsync(webSocket, $"{currentCryptocurrency}: {currentValue.ToString()}");
            }
        }

        public async Task SendMessageAsync(WebSocket socket, string message)
        {
            if (socket.State != WebSocketState.Open)
                return;

            await socket.SendAsync(buffer: new ArraySegment<byte>(array: Encoding.ASCII.GetBytes(message),
                                                                  offset: 0,
                                                                  count: message.Length),
                                   messageType: WebSocketMessageType.Text,
                                   endOfMessage: true,
                                   cancellationToken: CancellationToken.None);
        }
    }
}
