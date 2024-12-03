using Microsoft.AspNetCore.Mvc;
using PracticeWEBProject.Repository;
using PracticeWEBProject.Dto;

namespace PracticeWEBProject.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginRepository _loginRepository;

        public LoginController(LoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginPost(LoginViewModel loginModel)
        {

            try
            {
                // Attempt to authenticate the user
                var response = await _loginRepository.LoginAsync(loginModel);

                // Return success response with the response data
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log the exception message for debugging
                Console.WriteLine($"Login error: {ex.Message}");

                // Return an internal server error with a generic message
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

    }
}
