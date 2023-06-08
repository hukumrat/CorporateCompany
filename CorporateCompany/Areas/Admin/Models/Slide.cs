using System.ComponentModel.DataAnnotations;

namespace CorporateCompany.Areas.Admin.Models;

public class Slide
{
    [Key]
    public int Id { get; set; }
    public string Photo { get; set; }
    public string Header { get; set; }
}