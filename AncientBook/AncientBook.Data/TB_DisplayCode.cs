using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientBook.Data
{
    public partial class TB_DisplayCod : BaseEntity
    {
        [Key]
        public int DisplayCode { get; set; }

        [Required]
        [StringLength(50)]
        public string DisplayDescription { get; set; }
    }
}
