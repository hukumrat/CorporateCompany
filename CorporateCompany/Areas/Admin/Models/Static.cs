using System.ComponentModel.DataAnnotations;

namespace CorporateCompany.Areas.Admin.Models;

public class Static
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Lütfen hakkımızda özetini giriniz.")]
    [MaxLength(250, ErrorMessage = "Maksimum 250 karakter girilebilir.")]
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
}