using Dapper;
using Magicianred.Accounts.Domain.Factories;
using Magicianred.Accounts.Domain.Interfaces.Models;
using Magicianred.Accounts.Domain.Interfaces.Repositories;
using Magicianred.Accounts.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Magicianred.Accounts.DAL.Dapper.Repositories
{
    public class AccountRoleRepository : IAccountRoleRepository
    {
        private readonly IDatabaseConnectionFactory _connectionFactory;

        public AccountRoleRepository(IDatabaseConnectionFactory connectionFactory)
        {
            this._connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<IAccountRole>> GetAll()
        {
            IEnumerable<IAccountRole> accounts = null;
            using (var connection = _connectionFactory.GetConnection())
            {
                string sqlQuery = "SELECT AccountId, RoleId FROM AccountRoles";
                accounts = await connection.QueryAsync<AccountRole>(sqlQuery);
            }
            return accounts;
        }

        public async Task AddAsync(int accountId, int roleId)
        {
            var arList = new List<IRole>
            {
                new Role() { Id = roleId }
            };
            var accountRoles = arList.ToArray();
            await this.AddAsync(accountId, accountRoles);
        }

        public async Task AddAsync(int accountId, IRole[] accountRoles)
        {
            using var connection = _connectionFactory.GetConnection();
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("INSERT INTO AccountRoles (AccountId, RoleId) Values ");

            for (int i = 0; i < accountRoles.Length; i++)
            {
                if (i > 0) { sqlQuery.Append(","); }
                sqlQuery.Append(String.Format("({0},{1})", accountId, accountRoles[i].Id));
            }
            int rowsAffected = connection.Execute(sqlQuery.ToString());

            if (rowsAffected <= 0)
            {
                throw new ArgumentException("AccountRoleRepository.AddAsync.NoRowsAffected");
            }
        }

        public async Task AddAsync(IAccount account, IRole[] accountRoles)
        {
            await this.AddAsync(account.Id, accountRoles);
        }

        public async Task<IEnumerable<IAccountRole>> FindByAccountIdAsync(int accountId)
        {
            IEnumerable<AccountRole> accounts = null;
            using (var connection = _connectionFactory.GetConnection())
            {
                string sqlQuery = "SELECT AccountId, RoleId FROM AccountRoles WHERE AccountId = @AccountId";
                accounts = await connection.QueryAsync<AccountRole>(sqlQuery, new { AccountId = accountId });
            }
            return accounts;
        }
    }
}
