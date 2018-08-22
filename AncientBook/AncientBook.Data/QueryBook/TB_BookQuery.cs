using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientBook.Data.QueryBook
{
    public class TB_BookQuery
    {
        public Guid BookUid { get; set; }

        public string Title { get; set; }

        public string Title2 { get; set; }

        public string Volume { get; set; }

        public string Dynasty { get; set; }

        /// <summary>
        /// 目录表id
        /// </summary>
        public int CategoryId { get; set; }

        public string Functionary { get; set; }

        public string Publisher { get; set; }

        public string Version { get; set; }

        public string FromBF49 { get; set; }

        public string FromAF49 { get; set; }

      //  public string ImageUris { get; set; }

        public string Notice { get; set; }
        public DateTime CreateTime { get; set; }
        public string CategoryName { get; set; }
    }
}
