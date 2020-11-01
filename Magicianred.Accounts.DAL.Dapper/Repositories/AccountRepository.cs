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
    public class AccountRepository : IAccountRepository
    {
        private readonly IDatabaseConnectionFactory _connectionFactory;

        public AccountRepository(IDatabaseConnectionFactory connectionFactory)
        {
            this._connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<IAccount>> GetAll()
        {
            IEnumerable<Account> accounts = null;
            using (var connection = _connectionFactory.GetConnection())
            {
                string sqlQuery = "SELECT Id, Email, Password FROM Accounts";
                accounts = await connection.QueryAsync<Account>(sqlQuery);
            }
            return accounts;
        }

        public async Task AddAsync(IAccount account)
        {
            using var connection = _connectionFactory.GetConnection();
            string sqlQuery = "INSERT INTO Accounts (Email, Password, CreateDate) Values(@Email, @Password, @CreateDate)";
            int rowsAffected = connection.Execute(sqlQuery, account);

            if (rowsAffected != 1)
            {
                throw new ArgumentException("AccountRepository.AddAsync.NoRowsAffected");
            }
        }

        public async Task<IAccount> FindByEmailAsync(string email)
        {
            Account account = null;
            using (var connection = _connectionFactory.GetConnection())
            {
                string sqlQuery = "SELECT Id, Email, Password FROM Accounts WHERE Email = @Email";
                account = await connection.QueryFirstOrDefaultAsync<Account>(sqlQuery, new { Email = email });
            }
            return account;
        }
    }
}