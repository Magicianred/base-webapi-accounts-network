using Magicianred.Accounts.DAL.Dapper.MySql.Repositories;
using Magicianred.Accounts.Domain.Factories;
using Microsoft.Extensions.DependencyInjection;

namespace Magicianred.Accounts.DAL.Dapper.MySql.Extensions
{
    public static class DapperMySqlContextExtension
    {
        public static IServiceCollection AddMySqlContextSupport(this IServiceCollection services)
        {
            services.AddScoped<IDatabaseConnectionFactory, DatabaseConnectionFactory>();
            return services;
        }
    }
}
