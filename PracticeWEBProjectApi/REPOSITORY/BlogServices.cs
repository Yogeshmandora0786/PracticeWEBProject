using Dapper;
using Microsoft.EntityFrameworkCore;
using PracticeWEBProjectApi.DTO;
using PracticeWEBProjectApi.Interface;
using PracticeWEBProjectApi.Models;
using System.Data;

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
    }

    
}


