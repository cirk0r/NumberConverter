using System.Web.Mvc;
using NumberConverter.Service.Abstract;
using NumberConverter.ViewModel;

namespace NumberConverter.Controllers
{
    public class ConversionController : Controller
    {
        private readonly IConversionService _conversionService;
        public ConversionController(IConversionService conversionService)
        {
            _conversionService = conversionService;
        }

        public ActionResult Conversion()
        {
            return View(new ConversionViewModel());
        }

        [HttpPost]
        public ActionResult Conversion(ConversionViewModel model)
        {
            model.Conversion = _conversionService.Convert(model.Value);

            return PartialView("ConversionResult", model.Conversion);
        }
    }
}