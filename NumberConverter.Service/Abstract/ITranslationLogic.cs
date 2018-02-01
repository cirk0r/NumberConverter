using NumberConverter.ViewModel.Models;

namespace NumberConverter.Service.Abstract
{
    public interface ITranslationLogic
    {
        TranslationModel TranslationModel { get; }
        void ProcessDecimal(decimal value);
    }
}
