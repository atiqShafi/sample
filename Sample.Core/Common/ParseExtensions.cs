using System;

namespace Sample.Core.Common
{
    public static class ParseExtensions
    {
        public static bool IsDate(this string input)
        {
            if (string.IsNullOrEmpty(input)) return false;
            DateTime dt;
            return (DateTime.TryParse(input, out dt));
        }

        public static bool IsNumber(this string input,int? digits)
        {
            if (string.IsNullOrEmpty(input)) 
                return false;

            if (!digits.HasValue)            
            {
                int intNumber;
                return int.TryParse(input, out intNumber);
            }

            double doubleNumber;
            return (double.TryParse(input, out doubleNumber));            
        }
    }
}