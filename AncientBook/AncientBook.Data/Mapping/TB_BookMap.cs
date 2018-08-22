using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientBook.Data.Mapping
{
    public class TB_BookMap : EntityTypeConfiguration<TB_Book>
    {
        public TB_BookMap()
        {
            this.ToTable("TB_Book").HasKey(p => p.BookUid)
                .Ignore(p => p.HGUID);
            //this.Property(p => p.UserID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.HasRequired(p => p.Category).WithMany().HasForeignKey(p => p.CategoryId);
            //this.HasRequired(p => p.CmsStatus).WithMany().HasForeignKey(p => p.BookUid);
        }
    }
}
