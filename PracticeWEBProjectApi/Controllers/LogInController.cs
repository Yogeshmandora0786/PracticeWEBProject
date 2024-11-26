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
        public async Task<IActionResult> LoginActiveInactive([FromBody] LoginDTO loginDto)
        {
            try
            {
                if (loginDto == null || loginDto.Id == 0)
                {
                    return BadRequest("Invalid login data.");
                }

                var result = await _loginService.Login_Active_Inactive(loginDto);

                if (result == null)
                {
                    return NotFound("Login not found or failed to update.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
         [HttpPost]
        [Route("Login_Upsert")]
        public async Task<IActionResult> LoginUpsert([FromBody] LoginDTO login)
        {
            if (login == null)
            {
                return BadRequest("Login data is required.");
            }

            try
            {
                // Calling the Login_Upsert method in the service
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

