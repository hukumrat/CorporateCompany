using System.ComponentModel.DataAnnotations;

namespace CorporateCompany.Areas.Admin.Models;

public class Photo
{
    public int Id { get; set; }
    public int ContentId { get; set; }
    [Required]
    public string Path { get; set; }

    public Content Content { get; set; }
}