using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CorporateCompany.Areas.Admin.Models;

namespace CorporateCompany.Areas.Admin.Models;

public class Service
{
    [Key]
    public int ContentId { get; set; }
    [ForeignKey("ContentId")]
    public Content Content { get; set; }
    public string ShortDescription { get; set; }
}