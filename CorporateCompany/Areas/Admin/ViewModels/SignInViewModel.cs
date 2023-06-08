using System.ComponentModel.DataAnnotations;

namespace CorporateCompany.Areas.Admin.ViewModels
{
    public class SignInViewModel
    {
        [Required(ErrorMessage = "Lütfen kullanıcı adınızı giriniz.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Lütfen şifrenizi giriniz.")]
        [DataType(DataType.Password)]
        public string  Password { get; set; }
        
        [Display(Name = "Beni hatırla")]
        public bool RememberMe { get; set; }
    }
}
