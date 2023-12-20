using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace DTO.Utils
{
    public static class ExcelValidators
    {
        private const string phoneNumberPattern = @"^(0)[1-9][0-9]{8}$";
        private const string idCardPattern = @"^[0-9]{12}$";
        private static readonly CultureInfo vietnamCulture = new CultureInfo("vi-VN");

        public static bool IsNumber(string input)
        {
            return int.TryParse(input, out var n) && n > 0;
        }

        public static bool IsNonEmptyString(string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }

        public static bool IsPhoneNumber(string input)
        {
            return Regex.IsMatch(input, phoneNumberPattern);
        }

        public static bool IsIdCard(string input)
        {
            return Regex.IsMatch(input, idCardPattern);
        }

        public static bool IsDateTime(string value)
        {
            return DateTime.TryParseExact(value, "M/d/yyyy", vietnamCulture,
                DateTimeStyles.None, out _);
        }

    }
}
