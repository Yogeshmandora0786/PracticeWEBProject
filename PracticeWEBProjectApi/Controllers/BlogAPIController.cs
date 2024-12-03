using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeWEBProjectApi.Interface;
using PracticeWEBProjectApi.REPOSITORY;

namespace PracticeWEBProjectApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAPIController : ControllerBase
    {
        private readonly IBlog _blogServices;

        public BlogAPIController(IBlog blogServices)
        {
            _blogServices = blogServices;
        }

        [HttpGet]
        [Route("Blog_All")]
        public async Task<IActionResult> GetAllBlogs()
        {
            try
            {
                var blogs = await _blogServices.Blog_All();
                return Ok(blogs); 
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"An error occurred: {ex.Message}");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        [HttpGet]
        [Route("GetBlogById")]
        public async Task<IActionResult> GetBlogById(int Blogid)
        {
            try
            {
                var blog = await _blogServices.Blog_ById(Blogid);
                if (blog == null)
                {
                    return NotFound(new { Message = $"Blog with Id {Blogid} not found." });
                }

                return Ok(blog);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }
    }
}
