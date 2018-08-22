using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientBook.Data
{
    public partial class TB_CmsStatus : BaseEntity
    {
        [Key]
        public Guid CmsUid { get; set; }

        public int StatusId { get; set; }
    }
}
