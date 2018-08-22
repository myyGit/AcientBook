using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientBook.Data
{
    public partial class TB_LimitCode : BaseEntity
    {
        [Key]
        public int LimitCodeId { get; set; }

        [Required]
        [StringLength(50)]
        public string LimitDescription { get; set; }
    }
}
