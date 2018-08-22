using AncientBook.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientBook.Service
{
    public interface IUserAccountService
    {
        TB_UserAccount GetUserByUserName(string userName);
        TB_UserAccount GetUserByHGUID(string hguid);
        /// <summary>
        /// 获取所有用户列表
        /// </summary>
        /// <returns></returns>
        List<TB_UserAccount> GetALLUserList();
        void InsertUserAccount(TB_UserAccount user);
      //  int InsertUserAccountReturnID(TB_UserAccount user);
        void UpdateUser(TB_UserAccount user);

    }
}
