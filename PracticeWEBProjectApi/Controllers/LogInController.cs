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
    }
}
