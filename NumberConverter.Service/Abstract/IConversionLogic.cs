using NumberConverter.ViewModel.Models;

namespace NumberConverter.Service.Abstract
{
    public interface IConversionLogic
    {
        ConversionModel ConversionModel { get; }
        void ProcessDecimal(decimal value);
    }
}
