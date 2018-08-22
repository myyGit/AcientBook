using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientBook.Data
{
    public class TB_Category : BaseEntity
    {
        public int CategoryId { get; set; }

        [StringLength(20)]
        public string CategoryName { get; set; }
        public int PId { get; set; }
        public int Levels { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime LastChangeTime { get; set; }
        public bool IsDelete { get; set; }
        public List<TB_Category> Children { get; set; }

    }
}
