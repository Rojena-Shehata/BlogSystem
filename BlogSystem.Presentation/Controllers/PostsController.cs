using BlogSystem.ServicesAbstraction;
using BlogSystem.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSystem.Presentation.Controllers
{
    public class PostsController(IPostService _postService,ILogger<PostsController> _logger):ApiBaseController
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetAllPosts([FromQuery]PostQueryParameters queryParameters)
        {
            var sw = Stopwatch.StartNew();
            // your code
            var posts =await _postService.GetAllPosts(queryParameters);
            sw.Stop();
            _logger.LogInformation($"Request time: {sw.ElapsedMilliseconds} ms");
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostDTO>> GetPostById(int id)
        {
           var post=await _postService.GetPostById(id);
            return HandleResult<PostDTO>(post);
        }

    }
}
