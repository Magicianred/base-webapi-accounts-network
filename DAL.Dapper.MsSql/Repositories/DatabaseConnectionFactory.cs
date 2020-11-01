using Magicianred.Accounts.Domain.Factories;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Magicianred.Accounts.DAL.Dapper.MsSql.Repositories
{
    public class DatabaseConnectionFactory : IDatabaseConnectionFactory
    {
        private readonly IConfiguration _configuration;

        public DatabaseConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection GetConnection()
        {
            var connectionName = _configuration.GetSection("ConnectionName").Value;
            return new SqlConnection(_configuration.GetConnectionString(connectionName));
        }
    }
}
