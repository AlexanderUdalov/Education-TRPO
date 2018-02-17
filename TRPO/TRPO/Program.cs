using System;
using System.Globalization;

namespace DateRegex
{
    class Program
    {
        public const string Format = "yyyy-MM-ddTHH:mm:sszz";
        
        static void Main(string[] args)
        {
            Console.WriteLine("Enter date in ISO format:");
            
            try
            {
                DateTime inputDate = DateTime.ParseExact(Console.ReadLine(), Format, CultureInfo.InvariantCulture);

                Console.WriteLine("Next day date: " + DateInformer.GetNextDay(inputDate).ToString(Format));
                Console.WriteLine("Current week beggining date: " + DateInformer.GetCurrentWeekBegining(inputDate).ToString(Format));
                Console.WriteLine("Current week ending date: " + DateInformer.GetCurrentWeekEnding(inputDate).ToString(Format));
                Console.WriteLine("Next month date: " + DateInformer.GetNextMonth(inputDate).ToString(Format));
            }
            catch (FormatException fe)
            {
                Console.WriteLine("Incorrect format: " + fe.Message);
            }

            Console.ReadKey();
        }
    }
}
