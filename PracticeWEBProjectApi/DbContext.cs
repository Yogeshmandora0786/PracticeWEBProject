using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PracticeWEBProjectApi.Interface;
using PracticeWEBProjectApi.Models;
using System.Data;

namespace PracticeWEBProjectApi.Models
{
    public class DBContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DBContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnectionStrings");
        }

        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }
}
