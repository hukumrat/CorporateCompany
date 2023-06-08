using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CorporateCompany.Areas.Admin.Models;

public class Project
{
    [Key]
    public int ContentId { get; set; }
    public bool IsFinished { get; set; }
    [ForeignKey("ContentId")]
    public Content Content { get; set; }
}