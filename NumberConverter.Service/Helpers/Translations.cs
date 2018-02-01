using System.Collections.Generic;

namespace NumberConverter.Service.Helpers
{
    internal static class Translations
    {
        internal static List<string> GetDigitsList()
        {
            return new List<string>()
            {
                "zero",
                "one",
                "two",
                "three",
                "four",
                "five",
                "six",
                "seven",
                "eight",
                "nine"
            };
        }

        internal static List<string> GetTeensList()
        {
            return new List<string>()
            {
                "ten",
                "eleven",
                "twelve",
                "thirteen",
                "fourteen",
                "fifteen",
                "sixteen",
                "seventeen",
                "eighteen",
                "nineteen"
            };
        }

        internal static List<string> GetDozensList()
        {
            return new List<string>()
            {
                "",
                "ten",
                "twenty",
                "thirty",
                "fourty",
                "fifty",
                "sixty",
                "seventy",
                "eighty",
                "ninety"
            };
        }

        internal static string GetHundred()
        {
            return "hundred";
        }

        internal static List<string> GetTenMultiplesList()
        {
            return new List<string>()
            {
                "",
                "thousand",
                "million",
                "billion",
                "trillion",
                "quadrillion"
            };
        }
    }
}
