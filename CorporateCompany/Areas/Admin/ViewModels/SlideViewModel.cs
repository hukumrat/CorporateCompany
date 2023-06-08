using System.ComponentModel.DataAnnotations;
using CorporateCompany.ValidationAttributes;

namespace CorporateCompany.Areas.Admin.ViewModels
{
    public class SlideViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Lütfen bir görsel seçiniz.")]
        [AllowedExtensions(new[] { ".jpg", ".png", ".jpeg" })]
        public List<IFormFile> Photo { get; set; }
        public string? PhotoPath { get; set; }
        public string Header { get; set; }
    }
}
