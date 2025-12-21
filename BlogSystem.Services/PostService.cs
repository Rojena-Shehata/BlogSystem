using BlogSystem.Domain.Contracts;
using BlogSystem.Domain.Entities.Content;
using BlogSystem.ServicesAbstraction;
using BlogSystem.Shared.DTOs;
using MapsterMapper;


namespace BlogSystem.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PostService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PostDTO>> GetAllPosts()
        {
            var posts=await _unitOfWork.GetRepository<Post,int>().GetAllAsync();
            return  _mapper.Map<IEnumerable<Post>,IEnumerable< PostDTO>>(posts);
        }
    }
}
