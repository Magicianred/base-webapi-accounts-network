using Magicianred.Accounts.Domain.Interfaces.Models;
using Magicianred.Accounts.Domain.Interfaces.Services.Communication;
using System.Threading.Tasks;

namespace Magicianred.Accounts.Domain.Interfaces
{
    public interface IAccountService
    {
        Task<ICreateUserResponse> CreateAccountAsync(IAccount account, params IRole[] accountRoles);
        Task<IAccount> FindByEmailAsync(string email);
    }
}