using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
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
        public static string PreviousValue;
        public static HttpClient client = new HttpClient();

        public void Configure(IApplicationBuilder app)
        {
            app.UseWebSockets();
            app.Use(async (context, next) =>
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                    await Function(context, webSocket);
                }
                else
                {
                    context.Response.StatusCode = 400;
                }
            });
        }
        
        private async Task Function(HttpContext context, WebSocket webSocket)
        {
            while (webSocket.State == WebSocketState.Open)
            {
                var currentValue = await client.GetStringAsync(@"https://min-api.cryptocompare.com/data/price?fsym=BTC&tsyms=USD");
                await SendMessageAsync(webSocket, "Trying get values...");
                if (!currentValue.Equals(PreviousValue))
                {
                    PreviousValue = currentValue;
                    await SendMessageAsync(webSocket, currentValue.ToString());
                }
                await Task.Delay(3000);
            }
            await webSocket.CloseAsync(closeStatus: WebSocketCloseStatus.NormalClosure,
                                    statusDescription: "Closed succesfull",
                                    cancellationToken: CancellationToken.None);
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
