using Dapper;
using Magicianred.Accounts.Domain.Factories;
using Magicianred.Accounts.Domain.Interfaces.Models;
using Magicianred.Accounts.Domain.Interfaces.Repositories;
using Magicianred.Accounts.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Magicianred.Accounts.DAL.Dapper.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IDatabaseConnectionFactory _connectionFactory;

        public RoleRepository(IDatabaseConnectionFactory connectionFactory)
        {
            this._connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<IRole>> GetAll()
        {
            IEnumerable<IRole> roles = null;
            using (var connection = _connectionFactory.GetConnection())
            {
                string sqlQuery = "SELECT Id, Name, Description FROM Roles";
                roles = await connection.QueryAsync<Role>(sqlQuery);
            }
            return roles;
        }

        public async Task AddAsync(IRole role)
        {
            using var connection = _connectionFactory.GetConnection();
            
            string sqlQuery = "INSERT INTO Roles (Name, Description) Values(@Name, @Description)";
            int rowsAffected = connection.Execute(sqlQuery, role);

            if (rowsAffected != 1)
            {
                throw new ArgumentException("RoleRepository.AddAsync.NoRowsAffected");
            }
            
        }

        public async Task<IRole> FindByIdAsync(int id)
        {
            Role role = null;
            using (var connection = _connectionFactory.GetConnection())
            {
                string sqlQuery = "SELECT Id, Name, Description FROM Roles WHERE Id = @RoleId";
                role = await connection.QueryFirstOrDefaultAsync<Role>(sqlQuery, new { RoleId = id });
            }
            return role;
        }
    }
}