using System.ComponentModel.DataAnnotations;
using CorporateCompany.ValidationAttributes;

namespace CorporateCompany.Areas.Admin.ViewModels
{
    public class ReferenceAddViewModel
    {
        public ReferenceAddViewModel()
        {
            PhotosToUpload = new List<IFormFile>();
        }

        [Required(ErrorMessage = "Referans ismi giriniz.")]
        [MaxLength(200, ErrorMessage = "Referans ismi 200 karakterden fazla olamaz.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Referans açıklaması giriniz.")]
        public string Description { get; set; }

        [MaxFileSize(10 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".png", ".jpeg" })]
        public List<IFormFile> PhotosToUpload { get; set; }
    }
}
