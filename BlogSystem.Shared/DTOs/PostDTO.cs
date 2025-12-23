using BlogSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Shared.DTOs
{
    public record PostDTO
    {
        public int Id { get; init; }
        public string Content { get; init; } = default!;
        public string Title { get; init; } = default!;
        public DateTimeOffset CreatedAt { get; init; }= default!;//
        public DateTimeOffset? UpdatedAt { get; init; }= default!;//
        public string AuthorId { get; init; }=default!;
        public string AuthorName { get; init; } = default!;//*
        public ICollection<TagDTO> PostTags { get; init; }=default!;//*
        public string Category { get; init; } = default!;//*
        public string Status { get; init; } = default!;//


    }
}
