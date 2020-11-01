using Magicianred.Accounts.Domain.Interfaces.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Magicianred.Accounts.Domain.Interfaces.Repositories
{
    public interface IAccountRepository
    {
        Task<IEnumerable<IAccount>> GetAll();
        Task AddAsync(IAccount account);
        Task<IAccount> FindByEmailAsync(string email);
    }
}