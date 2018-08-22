using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientBook.Data.Mapping
{
    public class TB_CategoryMap : EntityTypeConfiguration<TB_Category>
    {
        public TB_CategoryMap()
        {
            this.ToTable("TB_Category").HasKey(p => p.CategoryId)
                .Ignore(p => p.HGUID);
            this.Property(p => p.CategoryId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
