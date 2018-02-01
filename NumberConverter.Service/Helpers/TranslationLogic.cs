using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using NumberConverter.Service.Abstract;
using NumberConverter.Service.Extensions;
using NumberConverter.ViewModel.Models;

namespace NumberConverter.Service.Helpers
{
    public class TranslationLogic : ITranslationLogic
    {
        private readonly TranslationModel _translationModel;

        public TranslationLogic()
        {
            _translationModel = new TranslationModel();
        }

        public TranslationModel TranslationModel => _translationModel;        

        /// <summary>
        /// Processes given decimal value and splits it to parts
        /// </summary>
        public void ProcessDecimal(decimal value)
        {
            if (value < 0)
                return;

            _translationModel.Value = value;

            var twoPartDecimalArray = _translationModel.Value.ToString().Split(char.Parse(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator));

            _translationModel.IntegerPart = Convert.ToInt64(twoPartDecimalArray?.First());
            _translationModel.DecimalPart = twoPartDecimalArray?.Length > 1
                ? Convert.ToInt32(_translationModel.Value.ToString().Split(char.Parse(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator))?.Last())
                : -1;
            _translationModel.IsDecimalPartEqualZero = _translationModel.DecimalPart <= 0;
            _translationModel.IntegerPartSplitted = _translationModel.IntegerPart.ToString().SplitToParts();

            if (_translationModel.PartsTranslations == null)
                _translationModel.PartsTranslations = new List<string>();

            for (int i = 0; i < _translationModel.IntegerPartSplitted.Length; i++)
            {
                _translationModel.PartsTranslations.Add(TranslatePartOfNumber(_translationModel.IntegerPartSplitted[i], _translationModel.IntegerPartSplitted.Length - i - 1));
            }
        }

        /// <summary>
        /// Translates given part of decimal into text
        /// </summary>
        /// <param name="part">Part of decimal (max 3 characters long)</param>
        /// <param name="partIndex">Index of part to indicate thousands, millions, etc.</param>
        /// <returns>Part translation</returns>
        public static string TranslatePartOfNumber(string part, int partIndex)
        {
            var rightDigit = Convert.ToInt32(part.Substring(part.Length - 1, 1));
            var middleDigit = part.Length > 1 ? Convert.ToInt32(part.Substring(part.Length - 2, 1)) : -1;
            var leftDigit = part.Length > 2 ? Convert.ToInt32(part.Substring(part.Length - 3, 1)) : -1;

            if (rightDigit == 0 && middleDigit == 0 && leftDigit == 0)
                return string.Empty;

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder = stringBuilder.Append(Translations.GetTenMultiplesList()[partIndex]);
            stringBuilder = middleDigit != 1
                ? stringBuilder.Insert(0, $"{Translations.GetDigitsList()[rightDigit]} ")
                : stringBuilder.Append(string.Empty);

            stringBuilder = part.Length > 1
                ? (middleDigit == 1 ?
                    stringBuilder.Insert(0, $"{Translations.GetTeensList()[rightDigit]} ")
                    : stringBuilder.Insert(0, $"{Translations.GetDozensList()[middleDigit]} "))
                : stringBuilder.Append(string.Empty);

            stringBuilder = part.Length > 2 && leftDigit > 0
                ? stringBuilder.Insert(0,
                    $"{Translations.GetDigitsList()[leftDigit]} {Translations.GetHundred()} ")
                : stringBuilder.Append(string.Empty);

            return stringBuilder.ToString().Trim();
        }
    }
}
