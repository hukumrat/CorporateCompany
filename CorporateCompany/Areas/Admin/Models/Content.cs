using System.ComponentModel.DataAnnotations;
using CorporateCompany.Areas.Admin.Models;

namespace CorporateCompany.Areas.Admin.Models;

public class Content
{
    public Content()
    {
        Photos = new List<Photo>();
    }
    public int Id { get; set; }
    [Required(ErrorMessage = "İsim giriniz.")]
    [MaxLength(200, ErrorMessage = "İsim 200 karakterden fazla olamaz.")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Açıklama giriniz.")]
    public string Description { get; set; }
    public List<Photo> Photos { get; set; }
}