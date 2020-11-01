using Magicianred.Accounts.Domain.Interfaces.Repositories;
using System.Threading.Tasks;

namespace Magicianred.Accounts.DAL.Dapper.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public async Task CompleteAsync()
        {
            // nothing todo
        }
    }
}
