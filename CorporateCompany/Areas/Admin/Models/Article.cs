using System.ComponentModel.DataAnnotations;

namespace CorporateCompany.Areas.Admin.Models;

public class Article
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
}