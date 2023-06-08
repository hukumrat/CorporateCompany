using System.ComponentModel.DataAnnotations;
using CorporateCompany.ValidationAttributes;

namespace CorporateCompany.Areas.Admin.ViewModels
{
    public class ServiceAddViewModel
    {
        public int  Id { get; set; }

        [Required(ErrorMessage = "Hizmet ismi giriniz.")]
        [MaxLength(200, ErrorMessage = "Hizmet ismi 200 karakterden fazla olamaz.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Hizmet açıklaması giriniz.")]
        public string Description { get; set; }

        public string ShortDescription { get; set; }

        [AllowedExtensions(new []{".jpg",".png",".jpeg"})]
        public List<IFormFile>? PhotosToUpload { get; set; }
    }
}
