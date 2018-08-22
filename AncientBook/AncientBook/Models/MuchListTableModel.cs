using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AncientBook.Models
{
    public class MuchListTableModel
    {
        public List<BookViewModel> BooViewModelList { get; set; }
        public int CurrentPageIndex { get; set; }
        public int PageSize { get; set; }
        /// <summary>
        /// 总数量
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageTotalCount { get; set; }
        public string KeyWord { get; set; }
        public string KeyJian { get; set; }
        public string KeyFan { get; set; }
        public string KeyWord2 { get; set; }
        public string KeyJian2 { get; set; }
        public string KeyFan2 { get; set; }
        public string Query { get; set; }
        public int SearchType { get; set; }
        
    }
}