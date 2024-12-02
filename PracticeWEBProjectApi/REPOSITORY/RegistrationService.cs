using PracticeWEBProjectApi.Interface;
using PracticeWEBProjectApi.Models;
using Dapper;
using System.Data;
using PracticeWEBProjectApi.DTO;
using PracticeWEBProjectApi.Controllers;

namespace PracticeWEBProjectApi.REPOSITORY
{
    public class RegistrationService : IRegistration
    {
        private readonly DBContext _dBContext;
        private readonly IConfiguration _configuration;

        public RegistrationService(DBContext dBContext, IConfiguration configuration)
        {
            _dBContext = dBContext;
            _configuration = configuration;
        }

        public async Task<List<RegistrationDTO>> Registration_All()
        {
            using (var connection = _dBContext.CreateConnection())
            {
                try
                {
                    var reg = await connection.QueryAsync<RegistrationDTO>("sp_Registration_All", commandType: CommandType.StoredProcedure);
                    return reg.ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    throw;
                }
            }
        }

        public async Task<RegistrationDTO> Registration_Upsert(RegistrationDTO reg)
        {
            using (var connection = _dBContext.CreateConnection())
            {
                try
                {
                    // Define the parameters for the stored procedure
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@Id", reg.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
                    param.Add("@FullName", reg.FullName, dbType: DbType.String, direction: ParameterDirection.Input);
                    param.Add("@Email", reg.Email, dbType: DbType.String, direction: ParameterDirection.Input);
                    param.Add("@Phone", reg.Phone, dbType: DbType.String, direction: ParameterDirection.Input);
                    param.Add("@Password", reg.Password, dbType: DbType.String, direction: ParameterDirection.Input);
                    param.Add("@IsActive", reg.IsActive, dbType: DbType.Boolean, direction: ParameterDirection.Input);
                    param.Add("@CreateDate", reg.CreateDate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
                    param.Add("@CreatedBy", reg.CreatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
                    param.Add("@UpdateDate", reg.UpdateDate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
                    param.Add("@UpdatedBy", reg.UpdatedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
                    param.Add("@DeleteDate", reg.DeleteDate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
                    param.Add("@DeletedBy", reg.DeletedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);
                    param.Add("@UserType", reg.UserType, dbType: DbType.String, direction: ParameterDirection.Input);
                    param.Add("@IsLock", reg.IsLock, dbType: DbType.Boolean, direction: ParameterDirection.Input);
                    param.Add("@UserName", reg.UserName, dbType: DbType.String, direction: ParameterDirection.Input);
                    // Execute the stored procedure and retrieve the result
                    var task = await connection.QueryMultipleAsync("sp_Registration_Upsert", param, commandTimeout: 600, commandType: CommandType.StoredProcedure);
                    return task.Read<RegistrationDTO>().FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw new Exception($"An error occurred while upserting registration: {ex.Message}", ex);
                }
            }
        }

        public async Task<RegistrationDTO> Registration_Active_Inactive(RegistrationDTO reg)
        {
            using (var connection = _dBContext.CreateConnection())
            {

                try
                {
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@Id", reg.Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
                    param.Add("@UpdatedBy", reg.DeletedBy, dbType: DbType.Int64, direction: ParameterDirection.Input);

                    var task = connection.QueryMultiple("sp_Registration_Active_Inactive", param, commandTimeout: 600, commandType: CommandType.StoredProcedure);
                    return task.Read<RegistrationDTO>().FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<RegistrationDTO> Registration_Delete(int id)
        {
            using (var connection = _dBContext.CreateConnection())
            {

                try
                {
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@Id", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
                   

                    var Task = connection.QueryMultiple("sp_GetById_Registration_Delete", param, commandTimeout: 600, commandType: CommandType.StoredProcedure);
                    return Task.Read<RegistrationDTO>().FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<RegistrationDTO> Registration_ById(int Id)
        {
            using (var connection = _dBContext.CreateConnection())
            {
                try
                {
                    RegistrationDTO commonResponse = new RegistrationDTO();

                    DynamicParameters param = new DynamicParameters();
                    param.Add("@Id", Id, dbType: DbType.Int64, direction: ParameterDirection.Input);
                    var area = await connection.QueryFirstOrDefaultAsync<RegistrationDTO>("sp_GetRegistration_ById", param, commandType: CommandType.StoredProcedure);
                    return area;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    throw;
                }
            }
        }

        public Task<RegistrationDTO> UpsertRegistration(RegistrationDTO reg)
        {
            throw new NotImplementedException();
        }
    }
}
