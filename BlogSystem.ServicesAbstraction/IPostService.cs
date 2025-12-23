using BlogSystem.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.ServicesAbstraction
{
    public interface IPostService
    {
        public Task<IEnumerable<PostDTO>> GetAllPosts(PostQueryParameters queryParameters);
    }
}
