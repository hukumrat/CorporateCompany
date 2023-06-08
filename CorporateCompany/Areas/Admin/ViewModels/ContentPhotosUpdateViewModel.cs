using System.ComponentModel.DataAnnotations;
using CorporateCompany.Areas.Admin.Models;
using CorporateCompany.ValidationAttributes;
using CorporateCompany.Areas.Admin.Models;

namespace CorporateCompany.Areas.Admin.ViewModels
{
    public class ContentPhotosUpdateViewModel
    {
        public ContentPhotosUpdateViewModel()
        {
            Photos = new List<Photo>();
            PhotosToUpload = new List<IFormFile>();
        }

        public int ContentId { get; set; }
        public string Name { get; set; }
        public List<Photo> Photos { get; set; }
   
        [Required(ErrorMessage = "Lütfen görsel seçiniz.")]
        [MaxFileSize(10 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".png", ".jpeg" })]
        public List<IFormFile> PhotosToUpload { get; set; }
    }
}
