using BlogSystem.Domain.Contracts;
using BlogSystem.Domain.Entities;
using BlogSystem.Domain.Entities.Content;
using BlogSystem.Services.Specifications.PostSpecifications;
using BlogSystem.ServicesAbstraction;
using BlogSystem.Shared.CommonResult;
using BlogSystem.Shared.DTOs;
using MapsterMapper;
using Microsoft.Extensions.Logging;
using System.Diagnostics;


namespace BlogSystem.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PostService> _logger;

        public PostService(IUnitOfWork unitOfWork,IMapper mapper,ILogger<PostService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<PostDTO>> GetAllPosts(PostQueryParameters queryParameters)
        {
            var sw = Stopwatch.StartNew();
            var specs = new ProjectionPostWithTagAndCategoryAndAuthorSpecification(queryParameters);
            sw.Stop();
            _logger.LogInformation($"time for create specification:{sw.ElapsedMilliseconds}========================");
            sw.Restart();
            var posts=await _unitOfWork.GetRepository<Post,int>().GetAllAsync(specs);
            sw.Stop();
            _logger.LogInformation($"time for geting posts:{sw.ElapsedMilliseconds}==================");
            
            //_unitOfWork.GetRepository<Post,int>().ExplicitLoading<Tag>(posts,x=>x.Tags)
            sw.Restart();
            sw.Stop();
            _logger.LogInformation($"time for Mapping posts:{sw.ElapsedMilliseconds}==============");
            return posts;
        }

        public async Task<Result<PostDTO>> GetPostById(int postId)
        {
            var spec = new PostWithTagAndCategoryAndAuthor(postId);
          var post= await _unitOfWork.GetRepository<Post,int>().GetByIdAsync(spec);
            if (post is null)
                return Error.NotFound("Post.NotFound",$"Post With Id:{postId} Was Not Found!");
               return _mapper.Map<PostDTO>(post);
           
        }
    }
}
