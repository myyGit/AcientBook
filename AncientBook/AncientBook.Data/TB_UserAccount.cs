using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientBook.Data
{
    public class TB_UserAccount : BaseEntity
    {
        [Key]
        public Guid UserUid { get; set; }

        [Required]
        [StringLength(20)]
        public string LoginId { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Tel { get; set; }
    }
}
