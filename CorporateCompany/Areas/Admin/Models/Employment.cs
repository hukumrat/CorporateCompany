using System.ComponentModel.DataAnnotations;
using CorporateCompany.ValidationAttributes;

namespace CorporateCompany.Areas.Admin.Models;

public class Employment
{
    public int Id { get; set; }

    public int EducationId { get; set; }
    [Required(ErrorMessage = "Lütfen isminizi giriniz.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Lütfen soyisminizi giriniz.")]
    public string Surname { get; set; }

    [Required(ErrorMessage = "Lütfen e posta adresinizi giriniz.")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Required(ErrorMessage = "Lütfen telefon numaranızı giriniz.")]
    [DataType(DataType.PhoneNumber)]
    public string Phone { get; set; }

    [Required(ErrorMessage = "Lütfen cinsiyetinizi belirtiniz.")]
    public bool Gender { get; set; }

    [Required(ErrorMessage = "Lütfen medeni durumunuzu belirtiniz.")]
    public bool Marriage { get; set; }

    [Required]
    [AllowedExtensions(new[] { ".pdf", ".doc", ".docx" })]
    [MaxFileSize(10*1024*1024)]
    public string Cv { get; set; }

    [Required] 
    public DateTime Date { get; set; }

    public Education Education { get; set; }
}