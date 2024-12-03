using Microsoft.AspNetCore.Mvc;
using PracticeWEBProjectApi.DTO;
using PracticeWEBProjectApi.Interface;
using PracticeWEBProjectApi.REPOSITORY;
using System.Threading.Tasks;

namespace PracticeWEBProjectApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogin _loginService;

        public LoginController(ILogin loginService)
        {
            _loginService = loginService;
        }


        [HttpPost]
        [Route("Login_Active_Inactive")]
        public async Task<ActionResult<RegistrationDTO>> Login_Active_Inactive(string UserName,string Password)
        {
         
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
            {
                return BadRequest("Username and Password are required.");
            }

            try
            {
                var loginDto = new LoginDTO
                {
                    UserName = UserName,
                    Password = Password
                };

                var result = await _loginService.Login_Active_Inactive(loginDto);

                if (result == null)
                {
                    return Unauthorized("Invalid username or password, or the account is locked.");
                }
                return Ok(result);  
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        //[HttpGet]
        //[Route("Login_Upsert")]
        //public async Task<IActionResult> Login_Upsert([FromQuery] string userName, [FromQuery] string password)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
        //        {
        //            return BadRequest("UserName and Password are required.");
        //        }

        //        var login = new LoginDTO
        //        {
        //            UserName = userName,
        //            Password = password
        //        };

        //        var result = await _loginService.Login_Upsert(login);

        //        if (result == null)
        //        {
        //            return NotFound("Unable to upsert login details.");
        //        }

        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}


    }
}

