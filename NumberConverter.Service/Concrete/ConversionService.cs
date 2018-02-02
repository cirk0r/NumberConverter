using System;
using System.Globalization;
using System.Linq;
using NumberConverter.Service.Abstract;

namespace NumberConverter.Service.Concrete
{
    public class ConversionService : IConversionService
    {
        private readonly IConversionLogic _conversionLogic;

        public ConversionService(IConversionLogic conversionLogic)
        {
            _conversionLogic = conversionLogic;
        }

        public string Convert(decimal value)
        {
            _conversionLogic.ProcessDecimal(value);

            if (!_conversionLogic?.ConversionModel?.PartsConversions?.Any() ?? false)
                return string.Empty;

            var integerPartConversion = string.Join(" ", _conversionLogic?.ConversionModel?.PartsConversions);

            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(integerPartConversion)
                + GetDecimalPartConversion();
        }

        private string GetDecimalPartConversion()
        {
            return (_conversionLogic.ConversionModel.IsDecimalPartEqualZero
                            ? string.Empty
                            : GetDecimalPartFormattedConversion());
        }

        private string GetDecimalPartFormattedConversion()
        {
            return $" and {_conversionLogic.ConversionModel.DecimalPart} / {Math.Pow(10, _conversionLogic.ConversionModel.DecimalPart.ToString().Length)}";
        }
    }
}
