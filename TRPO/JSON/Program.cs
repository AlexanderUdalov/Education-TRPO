using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Linq;

namespace JSON
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(TableWrapper));
            TableWrapper tableWrapper;

            using (FileStream fs = new FileStream(@"file.json", FileMode.OpenOrCreate))
            {
                tableWrapper = (TableWrapper)jsonFormatter.ReadObject(fs);
            }
            int tempIndex = tableWrapper.table.columnNames.TakeWhile(x => x != "temperature").Count();
            int timeIndex = tableWrapper.table.columnNames.TakeWhile(x => x != "time").Count();

            Console.WriteLine("MinTemp = " + tableWrapper.table.rows.Min(x => x[tempIndex]));
            Console.WriteLine("MaxTemp = " + tableWrapper.table.rows.Max(x => x[tempIndex]));
            Console.WriteLine("AvgTemp = " + tableWrapper.table.rows.Select(x => x[tempIndex]).Average(x => Convert.ToDouble(x)));

            Console.WriteLine("(MinTemp,Time) = " + tableWrapper.table.rows.Min(x => (x[tempIndex], x[timeIndex])));
            Console.WriteLine("(MaxTemp,Time) = " + tableWrapper.table.rows.Max(x => (x[tempIndex], x[timeIndex])));

            Console.ReadLine();
        }
    }
}
