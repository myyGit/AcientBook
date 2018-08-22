using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientBook.Data
{
    public partial class TB_UserAuthenCodes : BaseEntity
    {
        [Key]
        public Guid UserUid { get; set; }

        [StringLength(1000)]
        public string UserDisplayCodes { get; set; }

        [StringLength(1000)]
        public string UserLimitCodes { get; set; }
    }
}
