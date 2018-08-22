using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AncientBook.Data
{
    public partial class TB_CommentReply : BaseEntity
    {
        [Key]
        public Guid CommentUid { get; set; }

        public Guid TopicUidOrCommentUid { get; set; }

        public Guid PublisherUid { get; set; }

        [Required]
        [StringLength(500)]
        public string PublishContent { get; set; }

        public DateTime PublishTime { get; set; }
    }
}
