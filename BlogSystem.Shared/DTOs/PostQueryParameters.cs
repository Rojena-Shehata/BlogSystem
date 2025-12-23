using BlogSystem.Domain.Entities.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Shared.DTOs
{
    public class PostQueryParameters
    {
        public int? CategoryId { get; set; }
        public PostStatus? Status { get; set; }
        public int? TagId { get; set; }
        //private string _search;

        //public string MyProperty
        //{
        //    get { return _search; }
        //    set { _search = value.Trim().ToLower(); }
        //}

        public string? Search { get; set; }
    }
}
