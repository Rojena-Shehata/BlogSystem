using BlogSystem.Shared.CommonResult;
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
         Task<IEnumerable<PostDTO>> GetAllPosts(PostQueryParameters queryParameters);
        Task<Result<PostDTO>> GetPostById(int postId);
    }
}
