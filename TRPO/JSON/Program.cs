using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Linq;
using System.Collections.Generic;

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

            int tempIndex = tableWrapper.table.columnNames.IndexOf("temperature");
            int timeIndex = tableWrapper.table.columnNames.IndexOf("time");

            Dictionary<string, Dictionary<string, object>> result = new Dictionary<string, Dictionary<string, object>>()
            {
                ["temperature"] = new Dictionary<string, object>()
                {
                    ["start_date"] = new DateTime(),
                    ["end_date"] = new DateTime(),
                    ["num_records"] = 0,
                    ["min_temperature"] = Single.MaxValue,
                    ["min_time"] = new DateTime(),
                    ["max_temperature"] = Single.MinValue,
                    ["max_time"] = new DateTime(),
                    ["avg_temperature"] = 0
                }
            };

            foreach (var row in tableWrapper.table.rows)
            {
                if ((float)row[tempIndex] < (float)result["temperature"]["min_temperature"])
                {
                    result["temperature"]["min_temperature"] = row[tempIndex];
                    result["temperature"]["min_time"] = row[timeIndex];
                }
                if ((float)row[tempIndex] > (float)result["temperature"]["max_temperature"])
                {
                    result["temperature"]["max_temperature"] = row[tempIndex];
                    result["temperature"]["max_time"] = row[timeIndex];
                }
            }

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(result.GetType());

            serializer.WriteObject(Console.OpenStandardOutput(), result);
        }
    }
}
