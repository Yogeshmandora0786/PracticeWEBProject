using Dapper;
using Microsoft.EntityFrameworkCore;
using PracticeWEBProjectApi.DTO;
using PracticeWEBProjectApi.Interface;
using PracticeWEBProjectApi.Models;
using System.Data;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PracticeWEBProjectApi.REPOSITORY
{
    public class BlogServices : IBlog
    {
        private readonly DBContext _dBContext;
        private readonly IConfiguration _configuration;

        public BlogServices(DBContext dBContext, IConfiguration configuration)
        {
            _dBContext = dBContext;
            _configuration = configuration;
        }

        public async Task<List<BlogDTO>> Blog_All()
        {
            using (var connection = _dBContext.CreateConnection())
            {
                try
                {
                    var reg = await connection.QueryAsync<BlogDTO>("sp_Blog_All", commandType: CommandType.StoredProcedure);
                    return reg.ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    throw;
                }
            }
        }


        public async Task<BlogDTO> Blog_ById(int Blogid)
        {
            using (var connection = _dBContext.CreateConnection())
            {
                try
                {
                    BlogDTO commonResponse = new BlogDTO();

                    DynamicParameters param = new DynamicParameters();
                    param.Add("@Blogid", Blogid, dbType: DbType.Int64, direction: ParameterDirection.Input);
                    var area = await connection.QueryFirstOrDefaultAsync<BlogDTO>("sp_GetBlog_ById", param, commandType: CommandType.StoredProcedure);
                    return area;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    throw;
                }
            }
        }

        public async Task<CommonResponceDTO> Blog_Delete(int Blogid)
        {
            using (var connection = _dBContext.CreateConnection())
            {
                try
                {
                    CommonResponceDTO commonResponse = new CommonResponceDTO();

                    DynamicParameters param = new DynamicParameters();
                    param.Add("@Blogid", Blogid, dbType: DbType.Int64, direction: ParameterDirection.Input);
                    var area = await connection.QueryFirstOrDefaultAsync<CommonResponceDTO>("sp_GetById_Blog_Delete", param, commandType: CommandType.StoredProcedure);
                    return area;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    throw;
                }
            }
        }

        public async Task<CommonResponceDTO> Blog_active_inactive(int Blogid)
        {
            using (var connection = _dBContext.CreateConnection())
            {

                try
                {
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@Blogid", Blogid, dbType: DbType.Int64, direction: ParameterDirection.Input);
                 //   param.Add("'@IsActive", IsActive, dbType: DbType.Int64, direction: ParameterDirection.Input);

                    var area = await connection.QueryFirstOrDefaultAsync<CommonResponceDTO>("sp_Blog_Active_Inactive", param, commandType: CommandType.StoredProcedure);
                    return area;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<BlogDTO> Blog_Upsert(BlogDTO blog)
        {
            try
            {
                var connection = _dBContext.CreateConnection();
                // Define the parameters for the stored procedure
                DynamicParameters param = new DynamicParameters();
                param.Add("@Blogid", blog.Blogid, dbType: DbType.Int32, direction: ParameterDirection.Input);
                param.Add("@BlogTitle", blog.BlogTitle, dbType: DbType.String, direction: ParameterDirection.Input);
                param.Add("@BlogDescription", blog.BlogDescription, dbType: DbType.String, direction: ParameterDirection.Input);
                param.Add("@BlogImage", blog.BlogImage, dbType: DbType.String, direction: ParameterDirection.Input);
                    
                param.Add("@CreatedBy", blog.CreatedBy, dbType: DbType.String, direction: ParameterDirection.Input);

                param.Add("@UpdatedBy", blog.UpdatedBy, dbType: DbType.String, direction: ParameterDirection.Input);
            
                var task = await connection.QueryMultipleAsync("sp_Blog_Upsert", param, commandTimeout: 600, commandType: CommandType.StoredProcedure);
                var result = task.Read<BlogDTO>().FirstOrDefault();

                if (result != null)
                {
                    return result; // Return the blog details after upsert operation
                }
                else
                { 
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while upserting blog: {ex.Message}", ex);
            }
        }

    }


}


