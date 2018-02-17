using System;
using System.Collections.Generic;
using System.Text;

namespace DateRegex
{
    static class DateInformer
    {
        public static DateTime GetNextDay(DateTime current) => 
            new DateTime(current.Year, current.Month, current.Day).AddDays(1);
        
        public static DateTime GetNextMonth(DateTime current) => 
            new DateTime(current.Year, current.Month, 1).AddMonths(1);

        public static DateTime GetCurrentWeekBegining(DateTime current) =>
            new DateTime(current.Year, current.Month, current.Day).AddDays( - (int)current.DayOfWeek);

        public static DateTime GetCurrentWeekEnding(DateTime current) =>
            new DateTime(current.Year, current.Month, current.Day).AddDays(7 - (int)current.DayOfWeek).AddSeconds(-1);

        
    }
}
