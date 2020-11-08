using Dapper;
using Magicianred.Accounts.Domain.Enums;
using Magicianred.Accounts.Domain.Factories;
using Magicianred.Accounts.Domain.Interfaces.Models;
using Magicianred.Accounts.Domain.Interfaces.Models.Filters;
using Magicianred.Accounts.Domain.Interfaces.Models.OrderBy;
using Magicianred.Accounts.Domain.Interfaces.Repositories;
using Magicianred.Accounts.Domain.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
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

        public async Task<IEnumerable<IAccount>> GetAllAsync(List<IOrderBy> orderBy = null, List<string> includeProperties = null, int? skip = null, int? take = null)
        {
            IEnumerable<Account> accounts = null;
            var orderByToAdd = resolveOrderBy(orderBy);

            using (var connection = _connectionFactory.GetConnection())
            {
                string sqlQuery = $"SELECT Id, Email, Password FROM Accounts {orderByToAdd} ";
                accounts = await connection.QueryAsync<Account>(sqlQuery);
            }
            return accounts;
        }

        public async Task<IEnumerable<IAccount>> FindAsync(IAccountFilter filter = null, List<IOrderBy> orderBy = null, string includeProperties = null, int? skip = null, int? take = null)
        {
            throw new NotImplementedException();
        }

        public async Task<IAccount> GetFirstAsync(IAccountFilter filter = null, List<IOrderBy> orderBy = null, string includeProperties = null)
        {
            Account account = null;
            var filterOut = resolveFilter(filter);
            var filterToAdd = filterOut.Item1;
            var filterParams = filterOut.Item2;
            var orderByToAdd = resolveOrderBy(orderBy);

            using (var connection = _connectionFactory.GetConnection())
            {
                string sqlQuery = $"SELECT Id, Email, Password FROM Accounts {filterToAdd} {orderByToAdd} ";
                account = await connection.QueryFirstOrDefaultAsync<Account>(sqlQuery, filterParams);
            }
            return account;
        }

        public async Task<IAccount> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetCountAsync(IAccountFilter filter = null)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> GetExistsAsync(IAccountFilter filter = null)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(IAccount entity, EntityTypesEnum createdEntityTypeId, int? createdById = null)
        {
            using var connection = _connectionFactory.GetConnection();
            string sqlQuery = "INSERT INTO Accounts (Email, Password, CreateDate) Values(@Email, @Password, @CreateDate)";
            int rowsAffected = connection.Execute(sqlQuery, entity);

            if (rowsAffected != 1)
            {
                throw new ArgumentException("AccountRepository.AddAsync.NoRowsAffected");
            }
        }

        public async Task UpdateAsync(IAccount entity, EntityTypesEnum updatedEntityTypeId, int? updatedById = null)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int entityId, EntityTypesEnum deletedEntityTypeId, int? deletedById = null)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(IAccount entity, EntityTypesEnum deletedEntityTypeId, int? deletedById = null)
        {
            throw new NotImplementedException();
        }


        #region private methods
        private string resolveOrderBy(List<IOrderBy> orderBy = null)
        {
            StringBuilder orderByToAdd = new StringBuilder();
            bool hasOrderBy = false;

            if (orderBy != null)
            {
                for (int i = 0; i < orderBy.Count; i++)
                {
                    hasOrderBy = true;
                    if (i > 0) { orderByToAdd.Append(", "); }
                    var order = orderBy[i];

                    switch (order.Property.ToLower())
                    {
                        case "email":
                            orderByToAdd.Append(order.Sort == OrderByEnum.DESC ?
                                " Email DESC "
                                : " Email ASC ");
                            break;
                        default:
                            orderByToAdd.Append(" CreateDate DESC ");
                            break;
                    }
                }
            }
            if (hasOrderBy)
            {
                return $" ORDER BY {orderByToAdd.ToString()}";
            }
            return String.Empty;
        }

        private (string, object[]) resolveFilter(IAccountFilter filter)
        {
            dynamic paramsToAdd = new ExpandoObject();
            StringBuilder whereToAdd = new StringBuilder();
            bool hasWhere = false;

            if (filter != null)
            {
                if (filter.Id.HasValue)
                {
                    whereToAdd.Append(" Id = @AccountId ");
                    paramsToAdd.AccountId = filter.Id.Value;
                    hasWhere = true;
                }
                if (!String.IsNullOrEmpty(filter.Email))
                {
                    if (hasWhere) { whereToAdd.Append(" AND "); }
                    whereToAdd.Append(" Email = @Email ");
                    paramsToAdd.Email = filter.Email;
                    hasWhere = true;
                }
            }

            if (hasWhere)
            {
                return ($" WHERE {whereToAdd.ToString()}", paramsToAdd.ToArray());
            }
            return (String.Empty, paramsToAdd.ToArray());
        }
        #endregion
    }
}