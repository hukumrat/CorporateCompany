using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CorporateCompany.Areas.Admin.Models
{
    public class Reference
    {
        [Key]
        public int ContentId { get; set; }
        [ForeignKey("ContentId")]
        public Content Content { get; set; }
    }
}
