using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace JSON
{
    internal class Program
    {
        public static TableWrapper TableWrapper;

        static void Main(string[] args)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(TableWrapper));
            using (StreamReader r = new StreamReader(@"file.json"))
            {
                string json = r.ReadToEnd();
                TableWrapper = (TableWrapper) JsonConvert.DeserializeObject(json, typeof(TableWrapper));
            }
            //using (FileStream fs = new FileStream(@"file.json", FileMode.OpenOrCreate))
            //{
            //    TableWrapper = (TableWrapper)jsonFormatter.ReadObject(fs);
            //}
            //using (FileStream fs = new FileStream(@"file1.json", FileMode.OpenOrCreate))
            //{
            //    jsonFormatter.WriteObject(fs, TableWrapper);
            //}

            Console.WriteLine(
                JsonConvert.SerializeObject(
                    new Dictionary<string, Dictionary<string, object>>
                    {
                        {"current_speed", GetColumnStats("current_speed")},
                        {"temperature", GetColumnStats("temperature")},
                        {"salinity", GetColumnStats("salinity")}
                    },
                    Formatting.Indented)
                );
            Console.ReadLine();
        }

        public static Dictionary<string, object> GetColumnStats(string columnName)
        {
            int columnIndex = TableWrapper.table.columnNames.IndexOf(columnName);
            int columnQcIndex = TableWrapper.table.columnNames.IndexOf(columnName + "_qc");

            int minValueRowIndex = 0;
            int maxValueRowIndex = 0;

            double minValue = (double)TableWrapper.table.rows[0][columnIndex];
            double maxValue = (double)TableWrapper.table.rows[0][columnIndex];
            double sum = 0;
            int totalCount = 0;

            int rowIndex = 0;
            foreach (var row in TableWrapper.table.rows)
            {
                rowIndex++;
                if ((System.Int64) row[columnQcIndex] != 0)
                    continue;

                double currentValue = (double)row[columnIndex];
                
                if (currentValue < minValue)
                {
                    minValueRowIndex = rowIndex;
                    minValue = currentValue;
                }

                if (currentValue > maxValue)
                {
                    maxValueRowIndex = rowIndex;
                    maxValue = currentValue;
                }

                sum += currentValue;
                totalCount++;
            }

            var avgValue = sum / totalCount;
            int timeIndex = TableWrapper.table.columnNames.IndexOf("time");

            Dictionary<string, object> result = new Dictionary<string, object>(8)
            {
                {"start_date", DateTime.Parse(TableWrapper.table.rows[0][timeIndex].ToString()).ToShortDateString()},
                {"end_date", DateTime.Parse(TableWrapper.table.rows[TableWrapper.table.rows.Length - 1][timeIndex].ToString()).ToShortDateString()},
                {"num_records", totalCount},
                {"min_" + columnName, minValue},
                {"min_times", TableWrapper.table.rows[minValueRowIndex][timeIndex]},
                {"max_" + columnName, maxValue},
                {"max_times", TableWrapper.table.rows[maxValueRowIndex][timeIndex]},
                {"avg_" + columnName, avgValue}
            };

            return result;
        }
    }
}
