using System.ComponentModel.DataAnnotations;

namespace CorporateCompany.Areas.Admin.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Lütfen bir e-posta adresi giriniz.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lütfen bir kullanıcı adı giriniz.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Lütfen bir şifre giriniz.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Lütfen şifrenizi tekrar giriniz.")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "Şifreler uyuşmuyor.")]
        public string ConfirmPassword { get; set; }
    }
}
