using Microsoft.AspNetCore.Mvc;
using PracticeWEBProjectApi.DTO;

namespace PracticeWEBProjectApi.Interface
{
    public interface IRegistration
    {
        Task<List<RegistrationDTO>> Registration_All();

        Task<RegistrationDTO> Registration_Upsert(RegistrationDTO reg);

        Task<RegistrationDTO> Registration_Delete(int id);

        Task<RegistrationDTO> Registration_ById(int Id);
        //Task<RegistrationDTO> Registration_Active_Inactive(RegistrationDTO reg);
    }
}

