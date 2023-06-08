using System.ComponentModel.DataAnnotations;
using CorporateCompany.Areas.Admin.Models;
using CorporateCompany.Areas.Admin.Models;

namespace CorporateCompany.Areas.Admin.ViewModels
{
    public class StaticViewModel
    {
        public StaticViewModel()
        {
            Slides = new List<Slide>();
        }
        public int Id { get; set; }

        [Required(ErrorMessage = "Lütfen hakkımızda özetini giriniz.")]
        [MaxLength(250,ErrorMessage = "Maksimum 250 karakter girilebilir.")]
        public string AboutUsSummary { get; set; }

        [Required(ErrorMessage = "Lütfen isminizi giriniz.")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Lütfen bir adres giriniz.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Lütfen isminizi giriniz.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string LinkedIn { get; set; }
        public List<Slide> Slides { get; set; }
    }
}
