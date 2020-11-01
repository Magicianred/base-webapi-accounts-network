using Magicianred.Accounts.Domain.Interfaces.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Magicianred.Accounts.Domain.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        Task<IEnumerable<IRole>> GetAll();
        Task AddAsync(IRole role);
        Task<IRole> FindByIdAsync(int id);
    }
}
