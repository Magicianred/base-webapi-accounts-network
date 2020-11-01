using Magicianred.Accounts.DAL.Dapper.Repositories;
using Magicianred.Accounts.Domain.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Magicianred.Accounts.DAL.Dapper.Extensions
{
    public static class DapperContextExtension
    {
        public static IServiceCollection AddContextSupport(this IServiceCollection services)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IAccountRoleRepository, AccountRoleRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
