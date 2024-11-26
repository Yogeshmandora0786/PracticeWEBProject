using PracticeWEBProjectApi.Interface;
using PracticeWEBProjectApi.Models;
using Dapper;
using System.Data;
using PracticeWEBProjectApi.DTO;

namespace PracticeWEBProjectApi.REPOSITORY
{
    public class LoginService : ILogin
    {
        private readonly DBContext _dBContext;
        private readonly IConfiguration _configuration;

        public LoginService(DBContext dBContext, IConfiguration configuration)
        {
            _dBContext = dBContext;
            _configuration = configuration;
        }

        public async Task<RegistrationDTO> Login_Active_Inactive(LoginDTO log)
        {
            using (var connection = _dBContext.CreateConnection())
            {
                try
                {
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@Id", log.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);

                    var task = connection.QueryMultiple("sp_Login_Active_Inactive", param, commandTimeout: 600, commandType: CommandType.StoredProcedure);

                    var loginResult = task.Read<LoginDTO>().FirstOrDefault();

                    if (loginResult != null)
                    {
                            RegistrationDTO registration = new RegistrationDTO
                        {
                            Id = loginResult.Id,
                      
                            UserName = loginResult.UserName, 
                        };

                        return registration;
                    }

                    return null;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

    }
}
