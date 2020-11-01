using Magicianred.Accounts.Domain.Interfaces.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Magicianred.Accounts.Domain.Interfaces.Repositories
{
    public interface IAccountRoleRepository
    {
        Task<IEnumerable<IAccountRole>> GetAll();
        Task AddAsync(int accountId, int roleId);
        Task AddAsync(int accountId, IRole[] accountRoles);
        Task AddAsync(IAccount account, IRole[] accountRoles);
        Task<IEnumerable<IAccountRole>> FindByAccountIdAsync(int accountId);
    }
}
