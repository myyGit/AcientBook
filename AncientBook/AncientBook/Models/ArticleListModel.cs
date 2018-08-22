using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AncientBook.Models
{
    public class ArticleListModel
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
        public int CategoryId { get; set; }
    }
}