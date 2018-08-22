using AncientBook.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AncientBook.Models
{
    public class BookViewModel : TB_Book
    {
        public string CategoryName { get; set; }

        /// <summary>
        ///  folder name
        /// </summary>
       
        public string[] ImageUris { get; set; }

        /// <summary>
        /// imgsName
        /// </summary>
        public string[] ImageNames { get; set; }
    }
}