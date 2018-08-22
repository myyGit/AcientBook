using AncientBook.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientBook.Service
{
    public interface ICmsStatesService
    {
        IQueryable<Guid> GetDelGuidList();
        int InsertInfo(List<TB_CmsStatus> list);
    }
}
