using System.Collections.Generic;

namespace NumberConverter.ViewModel.Models
{
    public class TranslationModel
    {
        /// <summary>
        /// Value to be translated
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
        /// Contains integer part splitted into smaller parts of maximum 3 digits each for translation purposes
        /// </summary>
        public string[] IntegerPartSplitted { get; set; }

        /// <summary>
        /// True if decimal part of value is equal 0
        /// </summary>
        public bool IsDecimalPartEqualZero { get; set; }

        /// <summary>
        /// Translations of parts contained in IntegerPartSplitted array
        /// </summary>
        public List<string> PartsTranslations { get; set; }
    }
}
