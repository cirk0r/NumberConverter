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
    public class ConversionLogic : IConversionLogic
    {
        private readonly ConversionModel _conversionModel;

        public ConversionLogic()
        {
            _conversionModel = new ConversionModel();
        }

        public ConversionModel ConversionModel => _conversionModel;        

        /// <summary>
        /// Processes given decimal value and splits it to parts
        /// </summary>
        public void PrepareDecimalForConversion(decimal value)
        {
            if (value < 0)
                return;

            _conversionModel.Value = value;

            GetIntegerAndDecimalParts();
            SplitIntegerPart();
            CreateIntegerPartsConversions();
        }

        /// <summary>
        /// Converts given part of decimal into text
        /// </summary>
        /// <param name="part">Part of decimal (max 3 characters long)</param>
        /// <param name="partIndex">Index of part to indicate thousands, millions, etc.</param>
        /// <returns>Part conversion</returns>
        public static string ConvertPartOfNumber(string part, int partIndex)
        {
            int rightDigit, middleDigit, leftDigit;
            SplitNumberToDigits(part, out rightDigit, out middleDigit, out leftDigit);

            if (rightDigit == 0 && middleDigit == 0 && leftDigit == 0)
                return string.Empty;

            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder = AppendRightDigitConversion(partIndex, rightDigit, middleDigit, stringBuilder);
            stringBuilder = AppendMiddleDigitConversion(part, rightDigit, middleDigit, stringBuilder);
            stringBuilder = AppendLeftDigitConversion(part, leftDigit, stringBuilder);

            return stringBuilder.ToString().Trim();
        }

        private void CreateIntegerPartsConversions()
        {
            if (_conversionModel.PartsConversions == null)
                _conversionModel.PartsConversions = new List<string>();

            for (int i = 0; i < _conversionModel.IntegerPartSplitted.Length; i++)
            {
                _conversionModel.PartsConversions.Add(ConvertPartOfNumber(_conversionModel.IntegerPartSplitted[i], _conversionModel.IntegerPartSplitted.Length - i - 1));
            }

            _conversionModel.PartsConversions.RemoveAll(string.IsNullOrWhiteSpace);
        }

        private void SplitIntegerPart()
        {
            _conversionModel.IntegerPartSplitted = _conversionModel.IntegerPart.ToString().Split(StringExtensions.DefaultChunkSize);
        }

        private void GetIntegerAndDecimalParts()
        {
            //first array alement should contain integer part of given number, last should contain decimal part
            var twoPartDecimalArray = _conversionModel.Value.ToString().Split(char.Parse(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator));

            _conversionModel.IntegerPart = Convert.ToInt64(twoPartDecimalArray?.First());
            _conversionModel.DecimalPart = twoPartDecimalArray?.Length > 1
                ? Convert.ToInt32(_conversionModel.Value.ToString().Split(char.Parse(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator))?.Last())
                : -1;

            _conversionModel.IsDecimalPartEqualZero = _conversionModel.DecimalPart <= 0;
        }

        private static StringBuilder AppendLeftDigitConversion(string part, int leftDigit, StringBuilder stringBuilder)
        {
            stringBuilder = part.Length > 2 && leftDigit > 0
                ? stringBuilder.Insert(0,
                    $"{Conversions.GetDigitsNames()[leftDigit]} {Conversions.GetHundredName()} ")
                : stringBuilder.Append(string.Empty);
            return stringBuilder;
        }

        private static StringBuilder AppendMiddleDigitConversion(string part, int rightDigit, int middleDigit, StringBuilder stringBuilder)
        {
            stringBuilder = part.Length > 1
                ? (middleDigit == 1 ?
                    stringBuilder.Insert(0, $"{Conversions.GetTeenNumbersNames()[rightDigit]} ")
                    : stringBuilder.Insert(0, $"{Conversions.GetTensNames()[middleDigit]} "))
                : stringBuilder.Append(string.Empty);
            return stringBuilder;
        }

        private static StringBuilder AppendRightDigitConversion(int partIndex, int rightDigit, int middleDigit, StringBuilder stringBuilder)
        {
            stringBuilder = stringBuilder.Append(Conversions.GetTenMultiplesNames()[partIndex]);
            stringBuilder = middleDigit != 1
                ? stringBuilder.Insert(0, $"{Conversions.GetDigitsNames()[rightDigit]} ")
                : stringBuilder.Append(string.Empty);
            return stringBuilder;
        }

        private static void SplitNumberToDigits(string part, out int rightDigit, out int middleDigit, out int leftDigit)
        {
            rightDigit = Convert.ToInt32(part.Substring(part.Length - 1, 1));
            middleDigit = part.Length > 1 ? Convert.ToInt32(part.Substring(part.Length - 2, 1)) : -1;
            leftDigit = part.Length > 2 ? Convert.ToInt32(part.Substring(part.Length - 3, 1)) : -1;
        }
    }
}
