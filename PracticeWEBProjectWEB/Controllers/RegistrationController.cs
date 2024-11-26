using Microsoft.AspNetCore.Mvc;

namespace PracticeWEBProjectWEB.Controllers
{
    public class RegistrationController : Controller
    {

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ForgotPasswoerd()
        {
            return View();
        }
        
    }
}
