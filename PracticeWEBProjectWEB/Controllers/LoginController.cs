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
        [ActionName("Login")]
        public async Task<IActionResult> LoginPost(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _loginRepository.LoginAsync(loginModel);

                    if (response != null && response.IsSuccess)
                    {
                        // Redirect to home on successful login
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        // Return error if login fails
                        ModelState.AddModelError(string.Empty, "Invalid login attempt or user is inactive.");
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception and return a generic error
                    Console.WriteLine($"Login error: {ex.Message}");
                    ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again.");
                }
            }

            // Return the same view with validation errors
            return View("Login", loginModel);
        }
    }
}
