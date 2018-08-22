using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientBook.Data
{
    public class BookQueryData
    {
        public TypeEnum CategoryIdOne { get; set; }
        public string KeyWordOneSimp { get; set; }
        public string KeyWordOneOrig{ get; set; }
        public TypeEnum CategoryIdTwo { get; set; }
        public string KeyWordTwoSimp { get; set; }
        public string KeyWordTwoOrig { get; set; }
        /// <summary>
        /// 1:与  2：或
        /// </summary>
        public int SearchType { get; set; }
    }
    public enum TypeEnum 
    {
        /// <summary>
        /// 全部0
        /// </summary>
        [Description("全部")]
        All = 0,
        /// <summary>
        /// 书名1
        /// </summary>
        [Description("书名")]
        BookName = 1,
        /// <summary>
        /// 责任者2
        /// </summary>
        [Description("责任者")]
        Auther = 2,
        /// <summary>
        /// 出版者3
        /// </summary>
        [Description("出版者")]
        Publisher = 3,
        /// <summary>
        /// 朝代4
        /// </summary>
        [Description("朝代")]
        Dynasty = 4,
        /// <summary>
        /// 其他名称5
        /// </summary>
        [Description("其他名称")]
        OtherName = 5 
    }
}
