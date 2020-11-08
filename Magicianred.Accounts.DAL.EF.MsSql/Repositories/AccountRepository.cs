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
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _context;

        public AccountRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<IAccount>> FindAsync(IAccountFilter filter = null, List<IOrderBy> orderBy = null, string includeProperties = null, int? skip = null, int? take = null)
        {
            throw new NotImplementedException();
        }

        public async Task<IAccount> FindByEmailAsync(string email)
        {
            var filter = new AccountFilter();
            filter.Email = email;
            return await this.GetFirstAsync(filter);
        }

        public async Task<IEnumerable<IAccount>> GetAllAsync(List<IOrderBy> orderBy = null, List<string> includeProperties = null, int? skip = null, int? take = null)
        {
            var query = getQueryable();
            query = resolveOrderBy(query, orderBy);

            // fix: https://expertcodeblog.wordpress.com/2018/02/19/net-core-2-0-resolve-error-the-source-iqueryable-doesnt-implement-iasyncenumerable/
            return await Task.FromResult(query.ToList());
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

        public async Task<IAccount> GetFirstAsync(IAccountFilter filter = null, List<IOrderBy> orderBy = null, string includeProperties = null)
        {
            var query = getQueryable();
            query = resolveFilter(query, filter);
            query = resolveOrderBy(query, orderBy);
            return await Task.FromResult(query.FirstOrDefault());
        }

        public async Task CreateAsync(IAccount entity, EntityTypesEnum createdEntityTypeId, int? createdById = null)
        {
            _context.Accounts.Add((Account)entity);
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


        private IQueryable<Account> getQueryable()
        {
            return _context.Accounts.AsQueryable();
        }

        private IQueryable<Account> resolveOrderBy(IQueryable<Account> query, List<IOrderBy> orderBy = null)
        {
            if (orderBy != null)
            {
                foreach (var order in orderBy)
                {
                    switch (order.Property.ToLower())
                    {
                        case "email":
                            query = (order.Sort == OrderByEnum.DESC ?
                                query.OrderByDescending(o => o.Email).AsQueryable()
                                : query.OrderBy(o => o.Email).AsQueryable());
                            break;
                        default:
                            query = query.OrderByDescending(o => o.CreateDate).AsQueryable();
                            break;
                    }
                }
            }
            return query;
        }

        private IQueryable<Account> resolveFilter(IQueryable<Account> query, IAccountFilter filter)
        {
            if (filter != null)
            {
                if (filter.Id.HasValue)
                {
                    query = query.Where(w => w.Id == filter.Id.Value).AsQueryable();
                }
                if (!String.IsNullOrEmpty(filter.Email))
                {
                    query = query.Where(w => w.Email == filter.Email).AsQueryable();
                }
            }
            return query;
        }

        #endregion
    }
}