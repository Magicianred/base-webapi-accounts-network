using Magicianred.Accounts.DAL.EF.MsSql.Data;
using Magicianred.Accounts.DAL.EF.MsSql.Repositories;
using Magicianred.Accounts.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Magicianred.Accounts.DAL.EF.MsSql.Extensions
{
    public static class EFMsSqlContextExtension
    {
        public static IServiceCollection AddContextSupport(this IServiceCollection services, string connectionString, bool useInMemoryStore = false)
        {

            if (useInMemoryStore)
            {
                services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("base-webbapi"));
            }
            else
            {
                services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
            }
            // services.AddScoped<IAccountRepository>(_ => new AccountRepository(new AppDbContext(null)));
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IAccountRoleRepository, AccountRoleRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
