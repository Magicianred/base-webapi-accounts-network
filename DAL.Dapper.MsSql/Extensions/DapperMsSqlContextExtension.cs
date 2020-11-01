using Magicianred.Accounts.DAL.Dapper.MsSql.Repositories;
using Magicianred.Accounts.Domain.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace Magicianred.Accounts.DAL.Dapper.MsSql.Extensions
{
    public static class DapperMsSqlContextExtension
    {
        public static IServiceCollection AddMsSqlContextSupport(this IServiceCollection services)
        {
            services.AddScoped<IDatabaseConnectionFactory, DatabaseConnectionFactory>();
            return services;
        }
    }
}
