using System.Web.Mvc;
using NumberConverter.Service.Abstract;
using NumberConverter.ViewModel;

namespace NumberConverter.Controllers
{
    public class ConversionController : Controller
    {
        private readonly ITranslationService _translationService;
        public ConversionController(ITranslationService translationService)
        {
            _translationService = translationService;
        }

        public ActionResult Conversion()
        {
            return View(new ConversionViewModel());
        }

        [HttpPost]
        public ActionResult Conversion(ConversionViewModel model)
        {
            model.Translation = _translationService.Translate(model.Value);

            return PartialView("ConversionResult", model.Translation);
        }
    }
}