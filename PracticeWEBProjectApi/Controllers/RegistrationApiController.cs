using Microsoft.AspNetCore.Mvc;
using PracticeWEBProjectApi.DTO;
using PracticeWEBProjectApi.Interface;


namespace PracticeWEBProjectApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationApiController : ControllerBase
    {
        private readonly IRegistration _registrationService;

        public RegistrationApiController(IRegistration registrationService)
        {
            _registrationService = registrationService;
        }
        [HttpGet]
        [Route("Registration_All")]
        public async Task<IActionResult> GetAllRegistrations()
        {
            try
            {
                var registrations = await _registrationService.Registration_All();
                return Ok(registrations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving registrations.", details = ex.Message });
            }
        }
        [HttpPost]
        [Route("Registration_Upsert")]
        public async Task<IActionResult> Registration_Upsert([FromBody] RegistrationDTO reg)
        {
            try
            {
                var res = await _registrationService.Registration_Upsert(reg);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("Registration_Delete")]
        public async Task<IActionResult> Registration_Delete(RegistrationDTO reg)
        {
            try
            {
                var res = await _registrationService.Registration_Delete(reg);

                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("Registration_Active_Inactive")]
        public async Task<IActionResult> Registration_Active_Inactive(RegistrationDTO reg)
        {
            try
            {
                var res = await _registrationService.Registration_Active_Inactive(reg);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the area.", details = ex.Message });
            }
        }

        [HttpGet]
        [Route("Registration_ById/{Id}")]
        public async Task<IActionResult> GetRegistrationById(int Id)
        {
            try
            {
                var result = await _registrationService.Registration_ById(Id);

                if (result == null)
                    return NotFound($"Registration with ID {Id} not found.");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }
}

