using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RegexChecker
{
    internal class Program
    {
        public static readonly Dictionary<string, string> Patterns = new Dictionary<string, string>
        {
            ["EmailPattern"] = @"^(\w_-]+\.)*[\w_-]+@[\w_-]+(\.[\w_-]+)*\.[a-z]+$",
            ["DatePattern"] = @"^[0-9]{4}-((0?[1-9])|1[0-2])-(((0?)|1|2)[1-9]|3[01])$",
            ["DateAndTimePattern"] = @"^[0-9]{4}-(0[1-9]|1[0-2])-((0|1|2)[1-9]|3[01])[T| 017]?" +
                @"([01]\d|2[0-3])(:[0-5]\d){2}$"
        };

        static void Main(string[] args)
        {
            Console.WriteLine("Enter the line: ");

            var input = Console.ReadLine();

            foreach (var pattern in Patterns)
            {
                if (Regex.IsMatch(input, pattern.Value))
                    Console.WriteLine(pattern.Key);
            }

            Console.ReadKey();
        }
    }
}
