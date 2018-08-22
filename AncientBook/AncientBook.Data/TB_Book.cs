using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientBook.Data
{
    public class TB_Book : BaseEntity
    {
        [Key]
        public Guid BookUid { get; set; }
        //public virtual TB_CmsStatus CmsStatus { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(50)]
        public string Title2 { get; set; }

        public string Volume { get; set; }

        [StringLength(10)]
        public string Dynasty { get; set; }

        /// <summary>
        /// 目录表id
        /// </summary>
        public int CategoryId { get; set; }
        public virtual TB_Category Category { get; set; }

        [StringLength(20)]
        public string Functionary { get; set; }

        [StringLength(20)]
        public string Publisher { get; set; }

        [StringLength(20)]
        public string Version { get; set; }

        [StringLength(50)]
        public string FromBF49 { get; set; }

        [StringLength(50)]
        public string FromAF49 { get; set; }

        [StringLength(2000)]
        public string[] ImageUris { get; set; }

        [StringLength(200)]
        public string Notice { get; set; }
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 1:正常 2：删除
        /// </summary>
        public int IsDelete { get; set; }
        
    }
}
