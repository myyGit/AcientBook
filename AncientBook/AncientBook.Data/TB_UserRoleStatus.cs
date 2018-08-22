using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientBook.Data
{
    public partial class TB_UserRoleStatus : BaseEntity
    {
        [Key]
        public Guid UserUid { get; set; }

        public int RoleId { get; set; }

        public int StatusId { get; set; }

        public virtual TB_UserRoleName TB_UserRoleName { get; set; }

        public virtual TB_UserStatusName TB_UserStatusName { get; set; }
    }
}
