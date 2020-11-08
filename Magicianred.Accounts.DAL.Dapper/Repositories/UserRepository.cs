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
    public class UserRepository : IUserRepository
    {
        private readonly IDatabaseConnectionFactory _connectionFactory;

        public UserRepository(IDatabaseConnectionFactory connectionFactory)
        {
            this._connectionFactory = connectionFactory;
        }

        public async Task<IUser> FindByAccountIdAsync(int accountId)
        {
            User user = null;
            using (var connection = _connectionFactory.GetConnection())
            {
                StringBuilder sqlQuery = new StringBuilder();
                sqlQuery.Append("SELECT Id, Username, Name, Surname, Properties, CreateDate, LastAccess ");
                sqlQuery.Append(" FROM Users usrs ");
                sqlQuery.Append("WHERE Id = (SELECT UserId FROM UserAccounts WHERE usrs.AccountId = @AccountId)");
                user = await connection.QueryFirstOrDefaultAsync<User>(sqlQuery.ToString(), 
                        new { AccountId = accountId });
            }
            return user;
        }

        public async Task CreateAsync(IUser entity, EntityTypesEnum createdEntityTypeId, int? createdById = null)
        {
            throw new System.NotImplementedException();
        }

        public async Task DeleteAsync(int entityId, EntityTypesEnum deletedEntityTypeId, int? deletedById = null)
        {
            throw new System.NotImplementedException();
        }

        public async Task DeleteAsync(IUser entity, EntityTypesEnum deletedEntityTypeId, int? deletedById = null)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<IUser>> FindAsync(IUserFilter filter = null, List<IOrderBy> orderBy = null, string includeProperties = null, int? skip = null, int? take = null)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<IUser>> GetAllAsync(List<IOrderBy> orderBy = null, List<string> includeProperties = null, int? skip = null, int? take = null)
        {
            IEnumerable<User> users = null;
            var orderByToAdd = resolveOrderBy(orderBy);

            using (var connection = _connectionFactory.GetConnection())
            {
                string sqlQuery = $"SELECT Id, Username, Name, Surname, Properties, CreateDate, LastAccess FROM Users {orderByToAdd} ";
                users = await connection.QueryAsync<User>(sqlQuery);
            }
            return users;
        }

        public async Task<IUser> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> GetCountAsync(IUserFilter filter = null)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> GetExistsAsync(IUserFilter filter = null)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IUser> GetFirstAsync(IUserFilter filter = null, List<IOrderBy> orderBy = null, string includeProperties = null)
        {
            User user = null;
            var filterOut = resolveFilter(filter);
            var filterToAdd = filterOut.Item1;
            var filterParams = filterOut.Item2;
            var orderByToAdd = resolveOrderBy(orderBy);

            using (var connection = _connectionFactory.GetConnection())
            {
                string sqlQuery = $"SELECT Id, Username, Name, Surname, Properties, CreateDate, LastAccess FROM Users {filterToAdd} {orderByToAdd} ";
                user = await connection.QueryFirstOrDefaultAsync<User>(sqlQuery, filterParams);
            }
            return user;
        }

        public async Task UpdateAsync(IUser entity, EntityTypesEnum updatedEntityTypeId, int? updatedById = null)
        {
            throw new System.NotImplementedException();
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
                        case "id":
                            orderByToAdd.Append(order.Sort == OrderByEnum.DESC ?
                                " Id DESC "
                                : " Id ASC ");
                            break;
                        case "username":
                            orderByToAdd.Append(order.Sort == OrderByEnum.DESC ?
                                " Username DESC "
                                : " Username ASC ");
                            break;
                        case "name":
                            orderByToAdd.Append(order.Sort == OrderByEnum.DESC ?
                                " Name DESC "
                                : " Surname ASC ");
                            break;
                        case "lastaccess":
                            orderByToAdd.Append(order.Sort == OrderByEnum.DESC ?
                                " LastAccess DESC "
                                : " LastAccess ASC ");
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

        private (string, object[]) resolveFilter(IUserFilter filter)
        {
            dynamic paramsToAdd = new ExpandoObject();
            StringBuilder whereToAdd = new StringBuilder();
            bool hasWhere = false;

            if (filter != null)
            {
                if (filter.Id.HasValue)
                {
                    whereToAdd.Append(" Id = @UserId ");
                    paramsToAdd.AccountId = filter.Id.Value;
                    hasWhere = true;
                }
                if (!String.IsNullOrEmpty(filter.Username))
                {
                    if (hasWhere) { whereToAdd.Append(" AND "); }
                    whereToAdd.Append(" Username = @Username ");
                    paramsToAdd.Add(filter.Username);
                    hasWhere = true;
                }
                if (!String.IsNullOrEmpty(filter.Name))
                {
                    if (hasWhere) { whereToAdd.Append(" AND "); }
                    whereToAdd.Append(" Name = @Name ");
                    paramsToAdd.Add(filter.Name);
                    hasWhere = true;
                }
                if (!String.IsNullOrEmpty(filter.Surname))
                {
                    if (hasWhere) { whereToAdd.Append(" AND "); }
                    whereToAdd.Append(" Surname = @Surname ");
                    paramsToAdd.Add(filter.Surname);
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
