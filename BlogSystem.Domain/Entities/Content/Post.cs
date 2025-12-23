using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Domain.Entities.Content
{
    public class Post:BaseContentEntity<int>
    {
        public string Title { get; set; } = default!;
        public DateTimeOffset? UpdatedAt { get; set; }
        public PostStatus Status { get; set; }
        public int CategoryId { get; set; }//Fk
        public Category Category { get; set; } = default!;
        public ICollection<PostTag> PostTags { get; set; } = [];
        public ICollection<Tag> Tags { get; set; } = [];
        public ICollection<Comment> Comments { get; set; } = [];
    }
}
