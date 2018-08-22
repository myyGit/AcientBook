using AncientBook.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientBook.Service
{
    public class CmsStatesService : ICmsStatesService
    {
        private readonly IRepositoryBook<TB_CmsStatus> _cmsStatusRep;

        public CmsStatesService(IRepositoryBook<TB_CmsStatus> cmsStatusRep)
        {
            this._cmsStatusRep = cmsStatusRep;
        }
        public IQueryable<Guid> GetDelGuidList()
        {
            var a = _cmsStatusRep.Table.Where(p => p.StatusId == 2).Select(p => p.CmsUid);
            return a;
        }
        public int InsertInfo(List<TB_CmsStatus> list)
        {
             _cmsStatusRep.Insert(list);
             return 1;
        }
    }
}
