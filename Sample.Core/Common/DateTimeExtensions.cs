using System;
using System.Collections.Generic;
using System.Linq;

namespace Sample.Core.Common
{
    public static class DateTimeExtensions
    {
        public static bool IsWeekend(this DateTime dateTime)
        {
            return dateTime.DayOfWeek == DayOfWeek.Sunday || dateTime.DayOfWeek == DayOfWeek.Saturday;
        }

        public static bool IsInRange(this DateTime currentDate, DateTime beginDate, DateTime endDate)
        {
            return (currentDate >= beginDate && currentDate <= endDate);
        }

        public static bool IsBetween(this DateTime dt, DateTime startDate, DateTime endDate, Boolean compareTime = false)
        {
            return compareTime ?
               dt >= startDate && dt <= endDate :
               dt.Date >= startDate.Date && dt.Date <= endDate.Date;
        }

        public static DateTime First(this DateTime current)
        {
            var first = current.AddDays(1 - current.Day);
            return first;
        }

        public static DateTime First(this DateTime current, DayOfWeek dayOfWeek)
        {
            var first = current.First();

            if (first.DayOfWeek != dayOfWeek)
            {
                first = first.Next(dayOfWeek);
            }
            return first;
        }

        public static DateTime Last(this DateTime current)
        {
            var daysInMonth = DateTime.DaysInMonth(current.Year, current.Month);

            var last = current.First().AddDays(daysInMonth - 1);
            return last;
        }

        public static DateTime Next(this DateTime current, DayOfWeek dayOfWeek)
        {
            var offsetDays = dayOfWeek - current.DayOfWeek;

            if (offsetDays <= 0)
            {
                offsetDays += 7;
            }

            var result = current.AddDays(offsetDays);
            return result;
        }
    }
}