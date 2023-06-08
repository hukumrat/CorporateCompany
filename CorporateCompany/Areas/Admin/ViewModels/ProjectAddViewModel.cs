using System.ComponentModel.DataAnnotations;
using CorporateCompany.ValidationAttributes;

namespace CorporateCompany.Areas.Admin.ViewModels
{
    public class ProjectAddViewModel
    {
        public ProjectAddViewModel()
        {
            PhotosToUpload = new List<IFormFile>();
        }

        [Required(ErrorMessage = "Proje ismi giriniz.")]
        [MaxLength(200,ErrorMessage = "Proje ismi 200 karakterden fazla olamaz.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Proje açıklaması giriniz.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Proje durumunu belirtiniz.")]
        public bool IsFinished { get; set; }

        [MaxFileSize(10 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".png", ".jpeg" })]
        public List<IFormFile> PhotosToUpload { get; set; }
    }
}
