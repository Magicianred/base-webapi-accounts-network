using Magicianred.Accounts.DAL.EF.MsSql.Data;
using Magicianred.Accounts.Domain.Enums;
using Magicianred.Accounts.Domain.Interfaces.Models;
using Magicianred.Accounts.Domain.Interfaces.Models.Filters;
using Magicianred.Accounts.Domain.Interfaces.Models.OrderBy;
using Magicianred.Accounts.Domain.Interfaces.Repositories;
using Magicianred.Accounts.Domain.Models;
using Magicianred.Accounts.Domain.Models.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magicianred.Accounts.DAL.EF.MsSql.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IUser> FindByAccountIdAsync(int accountId)
        {
            User userFound = null;
            int? userId = _context.UserAccounts.Where(w => w.AccountId == accountId).Select(s => s.UserId).FirstOrDefault();
            if(userId.HasValue && userId > 0)
            {
                var filter = new UserFilter();
                filter.Id = userId.Value;
                return await this.GetFirstAsync(filter);
            }
            return await Task.FromResult(userFound);
        }

        public async Task CreateAsync(IUser entity, EntityTypesEnum createdEntityTypeId, int? createdById = null)
        {
            _context.Users.Add((User)entity);
        }

        public async Task DeleteAsync(int entityId, EntityTypesEnum deletedEntityTypeId, int? deletedById = null)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(IUser entity, EntityTypesEnum deletedEntityTypeId, int? deletedById = null)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<IUser>> FindAsync(IUserFilter filter = null, List<IOrderBy> orderBy = null, string includeProperties = null, int? skip = null, int? take = null)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<IUser>> GetAllAsync(List<IOrderBy> orderBy = null, List<string> includeProperties = null, int? skip = null, int? take = null)
        {
            throw new NotImplementedException();
        }

        public async Task<IUser> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetCountAsync(IUserFilter filter = null)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> GetExistsAsync(IUserFilter filter = null)
        {
            throw new NotImplementedException();
        }

        public async Task<IUser> GetFirstAsync(IUserFilter filter = null, List<IOrderBy> orderBy = null, string includeProperties = null)
        {
            var query = getQueryable();
            query = resolveFilter(query, filter);
            query = resolveOrderBy(query, orderBy);
            return await Task.FromResult(query.FirstOrDefault());
        }

        public async Task UpdateAsync(IUser entity, EntityTypesEnum updatedEntityTypeId, int? updatedById = null)
        {
            throw new NotImplementedException();
        }

        #region private methods


        private IQueryable<User> getQueryable()
        {
            return _context.Users.AsQueryable();
        }

        private IQueryable<User> resolveOrderBy(IQueryable<User> query, List<IOrderBy> orderBy = null)
        {
            if (orderBy != null)
            {
                foreach (var order in orderBy)
                {
                    switch (order.Property.ToLower())
                    {
                        case "username":
                            query = (order.Sort == OrderByEnum.DESC ?
                                query.OrderByDescending(o => o.Username).AsQueryable()
                                : query.OrderBy(o => o.Username).AsQueryable());
                            break;
                        case "name":
                            query = (order.Sort == OrderByEnum.DESC ?
                                query.OrderByDescending(o => o.Name).AsQueryable()
                                : query.OrderBy(o => o.Name).AsQueryable());
                            break;
                        case "surname":
                            query = (order.Sort == OrderByEnum.DESC ?
                                query.OrderByDescending(o => o.Surname).AsQueryable()
                                : query.OrderBy(o => o.Surname).AsQueryable());
                            break;
                        case "lastaccess":
                            query = (order.Sort == OrderByEnum.DESC ?
                                query.OrderByDescending(o => o.LastAccess).AsQueryable()
                                : query.OrderBy(o => o.LastAccess).AsQueryable());
                            break;
                        default:
                            query = query.OrderByDescending(o => o.CreateDate).AsQueryable();
                            break;
                    }
                }
            }
            return query;
        }

        private IQueryable<User> resolveFilter(IQueryable<User> query, IUserFilter filter)
        {
            if (filter != null)
            {
                if (filter.Id.HasValue)
                {
                    query = query.Where(w => w.Id == filter.Id.Value).AsQueryable();
                }
                if (!String.IsNullOrEmpty(filter.Username))
                {
                    query = query.Where(w => w.Username == filter.Username).AsQueryable();
                }
                if (!String.IsNullOrEmpty(filter.Name))
                {
                    query = query.Where(w => w.Name == filter.Name).AsQueryable();
                }
                if (!String.IsNullOrEmpty(filter.Surname))
                {
                    query = query.Where(w => w.Surname == filter.Surname).AsQueryable();
                }
            }
            return query;
        }

        #endregion
    }
}
