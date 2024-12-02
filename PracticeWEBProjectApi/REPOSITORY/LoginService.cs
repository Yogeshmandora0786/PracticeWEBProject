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

        public async Task<CommonResponceDTO> Login_Active_Inactive(LoginDTO log)
        {
            using (var connection = _dBContext.CreateConnection())
            {
                try
                {
                    // Prepare parameters for the stored procedure
                    var param = new DynamicParameters();
                    param.Add("@UserName", log.UserName, dbType: DbType.String, direction: ParameterDirection.Input);
                    param.Add("@Password", log.Password, dbType: DbType.String, direction: ParameterDirection.Input);

                    // Execute the stored procedure and retrieve the result
                    var result = await connection.QueryMultipleAsync(
                        "sp_Login_Active_Inactive",
                        param,
                        commandTimeout: 600,
                        commandType: CommandType.StoredProcedure
                    );

                    // Map the result to a CommonResponceDTO (assuming it needs to be mapped)
                    var response = result.Read<CommonResponceDTO>().FirstOrDefault();

                    return response; // Return the response, or null if no result is found
                }
                catch (Exception ex)
                {
                    // Log the exception for troubleshooting (optional, depends on logging framework)
                    throw new ApplicationException("An error occurred while processing the login request.", ex);
                }
            }
        }



        //public async Task<LoginDTO> Login_Upsert(LoginDTO login)
        //{
        //    using (var connection = _dBContext.CreateConnection())
        //    {
        //        try
        //        {
        //            // Define the parameters for the stored procedure
        //            DynamicParameters param = new DynamicParameters();
        //          //  param.Add("@Id", login.Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
        //            param.Add("@UserName", login.UserName, dbType: DbType.String, direction: ParameterDirection.Input);
        //            param.Add("@Password", login.Password, dbType: DbType.String, direction: ParameterDirection.Input);


        //            // Execute the stored procedure and retrieve the result
        //            var task = await connection.QueryMultipleAsync("sp_Login_Upsert", param, commandTimeout: 600, commandType: CommandType.StoredProcedure);
        //            return task.Read<LoginDTO>().FirstOrDefault();
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception($"only update can done: {ex.Message}", ex);
        //        }
        //    }
        //}


    }
}
