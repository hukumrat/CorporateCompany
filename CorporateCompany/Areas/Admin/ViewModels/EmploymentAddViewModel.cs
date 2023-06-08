using System.ComponentModel.DataAnnotations;
using CorporateCompany.ValidationAttributes;

namespace CorporateCompany.Areas.Admin.ViewModels
{
    public class EmploymentAddViewModel
    {
        public int EducationId { get; set; }
        [Required(ErrorMessage = "Lütfen isminizi giriniz.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Lütfen soy İsminizi giriniz.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Lütfen e posta adresinizi giriniz.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lütfen telefon numaranızı giriniz.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Lütfen geçerli bir telefon numarası giriniz.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Lütfen cinsiyetinizi belirtiniz.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Lütfen medeni durumunuzu belirtiniz.")]
        public string Marriage { get; set; }

        [Required(ErrorMessage = "Lütfen bir Cv yükleyiniz.")]
        [MaxFileSize(10 * 1024 * 1024)]
        [AllowedExtensions(new[] { ".pdf", ".doc", ".docx" })]
        public List<IFormFile> Cv { get; set; }

        public string? Description { get; set; }
    }
}
