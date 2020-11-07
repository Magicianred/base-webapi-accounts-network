using Magicianred.Accounts.Domain.Enums;
using Magicianred.Accounts.Domain.Interfaces.Models;
using Magicianred.Accounts.Domain.Interfaces.Models.Filters;
using Magicianred.Accounts.Domain.Interfaces.Models.OrderBy;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Magicianred.Accounts.Domain.Interfaces.Repositories
{
    public interface IAccountRepository
    {
        Task<IEnumerable<IAccount>> GetAllAsync(
            List<IOrderBy> orderBy = null,
            List<string> includeProperties = null,
            int? skip = null,
            int? take = null);

        Task<IEnumerable<IAccount>> FindAsync(
            IAccountFilter filter = null,
            List<IOrderBy> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null);

        Task<IAccount> GetFirstAsync(
            IAccountFilter filter = null,
            List<IOrderBy> orderBy = null,
            string includeProperties = null);

        Task<IAccount> GetByIdAsync(int id);

        Task<int> GetCountAsync(IAccountFilter filter = null);

        Task<bool> GetExistsAsync(IAccountFilter filter = null);

        Task CreateAsync(IAccount entity, EntityTypesEnum createdEntityTypeId, int? createdById = null);

        Task UpdateAsync(IAccount entity, EntityTypesEnum updatedEntityTypeId, int? updatedById = null);

        Task DeleteAsync(int entityId, EntityTypesEnum deletedEntityTypeId, int? deletedById = null);

        Task DeleteAsync(IAccount entity, EntityTypesEnum deletedEntityTypeId, int? deletedById = null);

        Task<IAccount> FindByEmailAsync(string email);
    }
}