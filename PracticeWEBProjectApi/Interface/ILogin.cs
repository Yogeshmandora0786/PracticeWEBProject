using PracticeWEBProjectApi.DTO;

namespace PracticeWEBProjectApi.Interface
{
    public interface ILogin // Change from class to interface
    {
        Task<RegistrationDTO> Login_Active_Inactive(LoginDTO log);
    }
}
