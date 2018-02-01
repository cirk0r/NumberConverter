using System;
using System.Globalization;
using System.Linq;
using NumberConverter.Service.Abstract;

namespace NumberConverter.Service.Concrete
{
    public class TranslationService : ITranslationService
    {
        private readonly ITranslationLogic _translationLogic;

        public TranslationService(ITranslationLogic translationLogic)
        {
            _translationLogic = translationLogic;
        }

        public string Translate(decimal value)
        {
            _translationLogic.ProcessDecimal(value);
            var nonEmptyTranslationParts = _translationLogic?.TranslationModel?.PartsTranslations?.Where(
                translation => !string.IsNullOrWhiteSpace(translation));

            if (nonEmptyTranslationParts == null)
                return string.Empty;

            var integerPartTranslation = string.Join(" ", nonEmptyTranslationParts);

            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(integerPartTranslation)
                + (_translationLogic.TranslationModel.IsDecimalPartEqualZero 
                ? string.Empty 
                : $" and {_translationLogic.TranslationModel.DecimalPart} / {Math.Pow(10, _translationLogic.TranslationModel.DecimalPart.ToString().Length)}");
        }
    }
}
