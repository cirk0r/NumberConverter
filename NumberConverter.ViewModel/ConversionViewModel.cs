using System.ComponentModel.DataAnnotations;

namespace NumberConverter.ViewModel
{
    public class ConversionViewModel
    {
        [DisplayFormat(DataFormatString = "{0:#.####}", ApplyFormatInEditMode = true)]
        [Display(Name = "Decimal value to be translated: ")]
        [Required]
        public decimal Value { get; set; }

        [Display(Name = "Translation: ")]
        public string Translation { get; set; }
    }
}