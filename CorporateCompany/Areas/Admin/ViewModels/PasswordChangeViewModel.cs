using System.ComponentModel.DataAnnotations;

namespace CorporateCompany.Areas.Admin.ViewModels
{
    public class PasswordChangeViewModel
    {
        [Required(ErrorMessage = "Lütfen mevcut şifrenizi giriniz.")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Lütfen yeni şifrenizi giriniz.")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Lütfen yeni şifrenizi tekrar giriniz.")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Şifreler uyuşmuyor.")]
        public string ConfirmNewPassword { get; set; }
    }
}
