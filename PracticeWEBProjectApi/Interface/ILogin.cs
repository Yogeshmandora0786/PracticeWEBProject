using PracticeWEBProjectApi.DTO;

namespace PracticeWEBProjectApi.Interface
{
    public interface ILogin
    {
        Task<RegistrationDTO> Login_Active_Inactive(LoginDTO log);
    }
}
