using System.Collections.Generic;

namespace NumberConverter.Service.Extensions
{
    public static class StringExtensions
    {
        public const int DefaultChunkSize = 3;

        /// <summary>
        /// Splits given text into smaller chunks with chunk size being specified by parameter
        /// </summary>
        /// <param name="text">Text to split</param>
        /// <param name="chunkSize">Size of chunk</param>
        /// <returns>Array of smaller chunks</returns>
        public static string[] Split(this string text, int chunkSize)
        {
            List<string> chunks = new List<string>();

            while (text.Length > 0)
            {
                var amountOfCharsToTake = text.Length % chunkSize == 0 ? 3 : text.Length % chunkSize;
                chunks.Add(text.Substring(0, amountOfCharsToTake));

                text = text.Remove(0, amountOfCharsToTake);
            }

            return chunks.ToArray();
        }
    }
}
