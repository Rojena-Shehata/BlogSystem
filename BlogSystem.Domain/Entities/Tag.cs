using BlogSystem.Domain.Entities.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Domain.Entities
{
    public class Tag:BaseEntity<int>
    {
        public string Name { get; set; } = default!;
        public List<Post> Posts { get; set; } = [];

    }
}
