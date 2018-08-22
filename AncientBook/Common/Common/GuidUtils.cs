using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class GuidUtils
    {
        public static string ClearLow
        {
            get
            {
                return Guid.NewGuid().ToString("N");
            }
        }

        public static string ToClearLow(string aGUID)
        {
            return aGUID.Replace("-", "").ToLower();
        }
    }
}
