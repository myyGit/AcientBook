using AncientBook.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientBook.Service
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IRepositoryUser<TB_UserAccount> _userRep;
        public UserAccountService(IRepositoryUser<TB_UserAccount> userRep)
        {
            this._userRep = userRep;
        }
        public TB_UserAccount GetUserByUserName(string userName)
        {
            return _userRep.Table.Where(p => p.LoginId == userName).FirstOrDefault();
        }

        public TB_UserAccount GetUserByHGUID(string hguid)
        {
            Guid guid = new Guid(hguid);
            return _userRep.Table.Where(p => p.UserUid == guid).FirstOrDefault();
        }


        public List<TB_UserAccount> GetALLUserList()
        {
            return _userRep.Table.ToList();
        }

        public void InsertUserAccount(TB_UserAccount user)
        {
            if (user != null)
            {
                _userRep.Insert(user);
            }
        }

        //public int InsertUserAccountReturnID(TB_UserAccount user)
        //{
        //    if (user != null)
        //    {
        //        return _userRep.InsertReturnID(user);
        //    }
        //    return 0;
        //}

        public void UpdateUser(TB_UserAccount user)
        {
            if (user != null)
            {
                _userRep.Update(user);
            }
        }
    }
}
