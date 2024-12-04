using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeWEBProjectApi.DTO;
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
        [HttpPost]
        [Route("DeleteBlog")]
        public async Task<IActionResult> DeleteBlog( int Blogid)
        {
            try
            {
                // Call the Blog_Delete method in the service
                var response = await _blogServices.Blog_Delete(Blogid);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        [HttpPost]
        [Route("ActivateDeactivate")]
        public async Task<IActionResult> BlogActiveInactive(int Blogid)
        {
            try
            {
                var response = await _blogServices.Blog_active_inactive(Blogid);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while processing your request.", Error = ex.Message });
            }
        }


        [HttpPost]
        [Route("Blog_Upsert")]
        public async Task<IActionResult> Blog_Upsert(BlogDTO blog)
        {
            try
            {
                string path = "";
                string ImagePathURL = "";

                try
                {
                    if (blog.ImagePath != null)
                    {

                        if (!string.IsNullOrEmpty(ImagePathURL) && System.IO.File.Exists(ImagePathURL))
                        {
                            System.IO.File.Delete(ImagePathURL);
                        }

                        path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "Images/BlogImages"));
                        var newFileName = DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(blog.ImagePath.FileName);
                        var fileSize = blog.ImagePath.Length;

                        if (fileSize > (5 * 1024 * 1024))
                        {
                            return BadRequest(new { Message = $"{blog.ImagePath.FileName}'s file size is larger than 5MB." });
                        }

                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }


                        using (var fileStream = new FileStream(Path.Combine(path, newFileName), FileMode.Create))
                        {
                            await blog.ImagePath.CopyToAsync(fileStream);
                        }

                        blog.BlogImage = Path.Combine("Images/BlogImages", newFileName);
                    }
                    else
                    {
                        blog.BlogImage = ImagePathURL; // Retain the old image if a new one isn't provided
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("File upload failed", ex);
                }

                var result = await _blogServices.Blog_Upsert(blog);

                if (result != null)
                {
                    return Ok(result);
                }

                return BadRequest(new { Message = "Blog upsert failed." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }
    }
}

