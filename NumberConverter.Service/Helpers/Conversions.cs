using System.Collections.Generic;

namespace NumberConverter.Service.Helpers
{
    internal static class Conversions
    {
        internal static List<string> GetDigitsNames()
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

        internal static List<string> GetTeenNumbersNames()
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

        internal static List<string> GetTensNames()
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

        internal static string GetHundredName()
        {
            return "hundred";
        }

        internal static List<string> GetTenMultiplesNames()
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
