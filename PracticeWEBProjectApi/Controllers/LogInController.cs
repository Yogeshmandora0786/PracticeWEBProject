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

        //[HttpGet]
        //[Route("Login_Active_Inactive")]
        //public async Task<IActionResult> Login_Active_Inactive([FromQuery] int id)
        //{
        //    try
        //    {
        //        if (id == 0)
        //        {
        //            return BadRequest("Invalid login ID.");
        //        }

        //        // Create a new LoginDTO with the provided ID
        //        var loginDto = new LoginDTO { Id = id };

        //        var result = await _loginService.Login_Active_Inactive(loginDto);

        //        if (result == null)
        //        {
        //            return NotFound("Login not found or failed to update.");
        //        }

        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}

        [HttpGet]
        [Route("Login_Upsert")]
        public async Task<IActionResult> Login_Upsert([FromQuery] string userName, [FromQuery] string password)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            {
                return BadRequest("UserName and Password are required.");
            }

            try
            {
                
                var login = new LoginDTO
                {
                    UserName = userName,
                    Password = password
                };

                
                var result = await _loginService.Login_Upsert(login);

                if (result == null)
                {
                    return NotFound("Login upsert failed.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }
}

