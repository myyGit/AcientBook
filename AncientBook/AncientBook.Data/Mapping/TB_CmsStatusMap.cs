using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientBook.Data.Mapping
{
    public class TB_CmsStatusMap : EntityTypeConfiguration<TB_CmsStatus>
    {
        public TB_CmsStatusMap()
        {
            this.ToTable("TB_CmsStatus").HasKey(p => p.CmsUid)
                .Ignore(p => p.HGUID);
        }
    }
}
