using BlogSystem.Domain.Entities.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Domain.Entities
{
    public class PostTag:BaseEntity<int>
    {
        public Post Post { get; set; } = default!;
        public int PostId { get; set; }
        public Tag Tag { get; set; } = default!;
        public int TagId { get; set; }
    }
}
