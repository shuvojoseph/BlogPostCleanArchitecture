using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogApp.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogsController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        // Public
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var blogs = await _blogService.GetAllBlogsAsync();
            return Ok(blogs);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var blog = await _blogService.GetByIdAsync(id);
            return blog == null ? NotFound() : Ok(blog);
        }

        // Authenticated
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateBlogRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                         ?? throw new Exception("Invalid user");

            var blog = await _blogService.AddBlogAsync(userId, request);
            return CreatedAtAction(nameof(GetById), new { id = blog.Id }, blog);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBlogRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                         ?? throw new Exception("Invalid user");

            var blog = await _blogService.UpdateBlogAsync(id, userId, request);
            return blog == null ? NotFound() : Ok(blog);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                         ?? throw new Exception("Invalid user");

            var success = await _blogService.DeleteBlogAsync(id, userId);
            return success ? NoContent() : NotFound();
        }
    }
}