using Magicianred.Accounts.Domain.Factories;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace Magicianred.Accounts.DAL.Dapper.MySql.Repositories
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
            return new MySqlConnection(_configuration.GetConnectionString(connectionName));
        }
    }
}
