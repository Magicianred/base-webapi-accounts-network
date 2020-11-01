using Magicianred.Accounts.DAL.EF.MsSql.Data;
using Magicianred.Accounts.Domain.Interfaces.Models;
using Magicianred.Accounts.Domain.Interfaces.Repositories;
using Magicianred.Accounts.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Magicianred.Accounts.DAL.EF.MsSql.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _context;

        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<IRole>> GetAll()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task AddAsync(IRole role)
        {
            _context.Roles.Add((Role)role);
        }

        public async Task<IRole> FindByIdAsync(int id)
        {
            return await _context.Roles.SingleOrDefaultAsync(r => r.Id == id);
        }
    }
}
