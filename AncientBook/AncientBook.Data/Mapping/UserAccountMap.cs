using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientBook.Data.Mapping
{
    public class UserAccountMap : EntityTypeConfiguration<TB_UserAccount>
    {
        public UserAccountMap()
        {
            this.HasKey(p => p.UserUid)
                .Ignore(p => p.HGUID);
            //this.Property(p => p.UserID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
