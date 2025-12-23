using BlogSystem.Domain.Contracts;
using BlogSystem.Domain.Entities;
using BlogSystem.Domain.Entities.Content;
using BlogSystem.Services.Specifications.PostSpecifications;
using BlogSystem.ServicesAbstraction;
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
    }
}
