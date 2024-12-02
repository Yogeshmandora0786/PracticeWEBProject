using PracticeWEBProjectApi.DTO;

namespace PracticeWEBProjectApi.Interface
{
    public interface ILogin
    {
        Task<CommonResponceDTO> Login_Active_Inactive(LoginDTO log);
    }
}
