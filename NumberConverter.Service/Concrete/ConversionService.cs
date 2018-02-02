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
            var nonEmptyConversionParts = _conversionLogic?.ConversionModel?.PartsConversions?.Where(
                conversion => !string.IsNullOrWhiteSpace(conversion));

            if (nonEmptyConversionParts == null)
                return string.Empty;

            var integerPartConversion = string.Join(" ", nonEmptyConversionParts);

            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(integerPartConversion)
                + GetDecimalPartConversion();
        }

        private string GetDecimalPartConversion()
        {
            return (_conversionLogic.ConversionModel.IsDecimalPartEqualZero
                            ? string.Empty
                            : $" and {_conversionLogic.ConversionModel.DecimalPart} / {Math.Pow(10, _conversionLogic.ConversionModel.DecimalPart.ToString().Length)}");
        }
    }
}
