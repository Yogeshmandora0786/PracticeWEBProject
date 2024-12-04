using Microsoft.AspNetCore.Mvc;
using PracticeWEBProject.Repository;
using PracticeWEBProjectWEB.Dto;

namespace PracticeWEBProject.Controllers
{
    public class RegisterController : Controller
    {
        private readonly RegisterRepository _registerRepository;

        public RegisterController(RegisterRepository registerRepository)
        {
            _registerRepository = registerRepository;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterPost(RegisterViewModel registerModel)
        {
            try
            {
                // Call repository method to handle registration
                var response = await _registerRepository.RegisterAsync(registerModel);

                // Return success response with the response data
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log the exception message for debugging
                Console.WriteLine($"Registration error: {ex.Message}");

                // Return an internal server error with a generic message
                return StatusCode(500, "An unexpected error occurred during registration. Please try again later.");
            }
        }
    }
}
