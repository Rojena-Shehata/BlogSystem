using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Domain.Entities.Content
{
    public class Comment:BaseContentEntity<int>
    {
        public Post Post { get; set; } = default!;
        public int PostId { get; set; }
    }
}
