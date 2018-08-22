using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AncientBook.common
{
    public class CommonInfo
    {
        private readonly bool isOpenLog = Convert.ToBoolean(ConfigurationManager.AppSettings["OpenLog"].ToString());
        public void WirteLog(string info)
        {
            if (isOpenLog)
            {
                Common.LogFile.WriteLogAddInfo(info + "*****");
            }
        }
    }
}