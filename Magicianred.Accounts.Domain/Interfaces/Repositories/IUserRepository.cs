using Magicianred.Accounts.Domain.Enums;
using Magicianred.Accounts.Domain.Interfaces.Models;
using Magicianred.Accounts.Domain.Interfaces.Models.Filters;
using Magicianred.Accounts.Domain.Interfaces.Models.OrderBy;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Magicianred.Accounts.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<IUser>> GetAllAsync(
            List<IOrderBy> orderBy = null,
            List<string> includeProperties = null,
            int? skip = null,
            int? take = null);

        Task<IEnumerable<IUser>> FindAsync(
            IUserFilter filter = null,
            List<IOrderBy> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null);

        Task<IUser> GetFirstAsync(
            IUserFilter filter = null,
            List<IOrderBy> orderBy = null,
            string includeProperties = null);

        Task<IUser> GetByIdAsync(int id);

        Task<int> GetCountAsync(IUserFilter filter = null);

        Task<bool> GetExistsAsync(IUserFilter filter = null);

        Task CreateAsync(IUser entity, EntityTypesEnum createdEntityTypeId, int? createdById = null);

        Task UpdateAsync(IUser entity, EntityTypesEnum updatedEntityTypeId, int? updatedById = null);

        Task DeleteAsync(int entityId, EntityTypesEnum deletedEntityTypeId, int? deletedById = null);

        Task DeleteAsync(IUser entity, EntityTypesEnum deletedEntityTypeId, int? deletedById = null);

        Task<IUser> FindByAccountIdAsync(int accountId);
    }
}
