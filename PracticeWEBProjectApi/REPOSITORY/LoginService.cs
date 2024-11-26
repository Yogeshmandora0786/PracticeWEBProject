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

        public async Task<LoginDTO> Login_Upsert(LoginDTO login)
        {
            using (var connection = _dBContext.CreateConnection())
            {
                try
                {
                    // Define the parameters for the stored procedure
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@Id", login.Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    param.Add("@UserName", login.UserName, dbType: DbType.String, direction: ParameterDirection.Input);
                    param.Add("@Password", login.Password, dbType: DbType.String, direction: ParameterDirection.Input);
                   

                    // Execute the stored procedure and retrieve the result
                    var task = await connection.QueryMultipleAsync("sp_Login_Upsert", param, commandTimeout: 600, commandType: CommandType.StoredProcedure);
                    return task.Read<LoginDTO>().FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw new Exception($"An error occurred while upserting login: {ex.Message}", ex);
                }
            }
        }


    }
}
