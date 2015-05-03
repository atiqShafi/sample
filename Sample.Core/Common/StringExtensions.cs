using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Sample.Core.Common
{
    public static class StringExtensions
    {
        public static string FormatWith(this string format, params object[] args)
        {
            return string.Format(format, args);
        }

        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsNotEmpty(this string value)
        {
            return !value.IsEmpty();
        }

        public static string NullIfEmpty(this string value)
        {
            if (value == string.Empty)
                return null;

            return value;
        }
        public static string EmptyIfNull(this string value)
        {
            if (value == null)
            {
                return "";
            }

            return value;
        }

        public static string SeparatePascalCase(this string value)
        {
            return Regex.Replace(value, "([A-Z])", " $1").Trim();
        }


        public static string Repeat(this string stringToRepeat, int repeat)
        {
            var builder = new StringBuilder(repeat*stringToRepeat.Length);
            for (int i = 0; i < repeat; i++)
            {
                builder.Append(stringToRepeat);
            }
            return builder.ToString();
        }

        public static string RemoveDiacritics(this String s)
        {
            var normalizedString = s.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark))
            {
                stringBuilder.Append(c);
            }

            return stringBuilder.ToString();
        }

        public static string UppercaseFirst(this string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        public static string ToPascalCase(this string stringToConvert)
        {
            // If there are 0 or 1 characters, just return the string.
            if (stringToConvert == null) 
                return null;
            
            if (stringToConvert.Length < 2) 
                return stringToConvert.ToUpper();

            // Split the string into words.
            var words = stringToConvert.Split(new char[] {}, StringSplitOptions.RemoveEmptyEntries);

            return words.Aggregate("", (current, word) => current + (word.Substring(0, 1).ToUpper() + word.Substring(1)));
        }

        public static bool IsEmail(this string s)
        {
            var regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(s);
        }

        public static string ReplaceFirst(this string text, string search, string replace)
        {
            var position = text.IndexOf(search, StringComparison.Ordinal);
            if (position < 0)
            {
                return text;
            }
            return text.Substring(0, position) + replace + text.Substring(position + search.Length);
        }
    }
}