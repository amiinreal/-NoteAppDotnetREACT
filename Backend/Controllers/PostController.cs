// Controllers/PostController.cs
using Microsoft.AspNetCore.Mvc;
using NoteApp.Models;
using NoteApp.Repositories;
using System;
using System.IO;
using System.Threading.Tasks;

namespace NoteApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly ILogger<PostController> _logger;

        public PostController(IPostRepository postRepository, ILogger<PostController> logger)
        {
            _postRepository = postRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            try
            {
                var posts = await _postRepository.GetPostsAsync();
                return Ok(posts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching posts.");
                return StatusCode(500, new { success = false, message = "An error occurred while fetching posts." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromForm] string Content, [FromForm] string Username, [FromForm] IFormFile? Image)
        {
            try
            {
                if (string.IsNullOrEmpty(Content) || string.IsNullOrEmpty(Username))
                {
                    return BadRequest(new { success = false, message = "Content and Username are required." });
                }

                var post = new Post
                {
                    Content = Content,
                    Username = Username,
                    CreatedAt = DateTime.UtcNow
                };

                if (Image != null && Image.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await Image.CopyToAsync(memoryStream);
                        post.ImageData = memoryStream.ToArray();
                    }
                }

                await _postRepository.CreatePostAsync(post);
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a post.");
                return StatusCode(500, new { success = false, message = "An error occurred while creating a post." });
            }
        }
    }
}