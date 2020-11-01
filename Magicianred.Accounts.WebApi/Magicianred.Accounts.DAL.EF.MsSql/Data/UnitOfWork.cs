using Magicianred.Accounts.Domain.Interfaces.Repositories;
using System.Threading.Tasks;

namespace Magicianred.Accounts.DAL.EF.MsSql.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}