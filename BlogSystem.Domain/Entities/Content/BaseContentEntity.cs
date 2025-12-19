using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Domain.Entities.Content
{
    public abstract class BaseContentEntity<Tkey>:BaseEntity<Tkey>
    {
        public string Content { get; set; } = default!;
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public string AuthorId { get; set; } = default!;//Fk
        public ApplicationUser Author { get; set; } = default!;

    }
}
