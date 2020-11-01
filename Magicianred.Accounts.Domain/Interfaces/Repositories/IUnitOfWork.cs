using System.Threading.Tasks;

namespace Magicianred.Accounts.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}