using System.Collections.Generic;

namespace NumberConverter.ViewModel.Models
{
    public class ConversionModel
    {
        /// <summary>
        /// Value to be converted
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// Integer part of value
        /// </summary>
        public long IntegerPart { get; set; }

        /// <summary>
        /// Decimal part of value
        /// </summary>
        public int DecimalPart { get; set; }

        /// <summary>
        /// Contains integer part splitted into smaller parts of maximum 3 digits each for conversion purposes
        /// </summary>
        public string[] IntegerPartSplitted { get; set; }

        /// <summary>
        /// True if decimal part of value is equal 0
        /// </summary>
        public bool IsDecimalPartEqualZero { get; set; }

        /// <summary>
        /// Conversions of parts contained in IntegerPartSplitted array
        /// </summary>
        public List<string> PartsConversions { get; set; }
    }
}
