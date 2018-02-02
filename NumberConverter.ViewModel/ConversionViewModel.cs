using System.ComponentModel.DataAnnotations;

namespace NumberConverter.ViewModel
{
    public class ConversionViewModel
    {
        [DisplayFormat(DataFormatString = "{0:#.####}", ApplyFormatInEditMode = true)]
        [Display(Name = "Decimal value to be converted: ")]
        [Required]
        public decimal Value { get; set; }

        [Display(Name = "Conversion: ")]
        public string Conversion { get; set; }
    }
}