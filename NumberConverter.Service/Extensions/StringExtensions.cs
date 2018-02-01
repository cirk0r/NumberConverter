using System.Collections.Generic;

namespace NumberConverter.Service.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Splits given text into smaller parts of maximum 3 characters length each
        /// </summary>
        /// <param name="text">Text to split</param>
        /// <returns>Array of smaller parts</returns>
        public static string[] SplitToParts(this string text)
        {
            List<string> triples = new List<string>();

            while (text.Length > 0)
            {
                var amountOfCharsToTake = text.Length % 3 == 0 ? 3 : text.Length % 3;
                triples.Add(text.Substring(0, amountOfCharsToTake));

                text = text.Remove(0, amountOfCharsToTake);
            }

            return triples.ToArray();
        }
    }
}
