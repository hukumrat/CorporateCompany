using System.ComponentModel.DataAnnotations;

namespace CorporateCompany.Areas.Admin.ViewModels
{
    public class ProjectUpdateViewModel
    {
        [Required(ErrorMessage = "Proje durumunu belirtiniz.")]
        public bool IsFinished { get; set; }

        public int ContentId { get; set; }

        [Required(ErrorMessage = "Proje ismi giriniz.")]
        [MaxLength(200, ErrorMessage = "Proje ismi 200 karakterden fazla olamaz.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Proje açıklaması giriniz.")]
        public string  Description { get; set; }
    }
}
