using System.ComponentModel.DataAnnotations;

namespace CorporateCompany.Areas.Admin.ViewModels
{
    public class ContactUsViewModel
    {
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? CompanyEmail { get; set; }

        public int Id { get; set; }
        [Required(ErrorMessage = "Lütfen isminizi giriniz.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Lütfen soyisminizi giriniz.")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Lütfen e-posta adresinizi giriniz.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Lütfen mesajınızı giriniz.")]
        [MaxLength(3000)]
        public string Messages { get; set; }
    }
}
