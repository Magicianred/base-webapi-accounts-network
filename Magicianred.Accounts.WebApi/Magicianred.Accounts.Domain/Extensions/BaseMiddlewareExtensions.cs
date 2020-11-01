using Magicianred.Accounts.Domain.Handlers;
using Magicianred.Accounts.Domain.Interfaces;
using Magicianred.Accounts.Domain.Interfaces.Handlers;
using Magicianred.Accounts.Domain.Interfaces.Models;
using Magicianred.Accounts.Domain.Interfaces.Services.Communication;
using Magicianred.Accounts.Domain.Models;
using Magicianred.Accounts.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Magicianred.Accounts.Domain.Extensions
{
    public static class BaseMiddlewareExtensions
    {
        public static IServiceCollection AddBaseDomain(this IServiceCollection services)
        {
            services.AddScoped<IAccount, Account>();
            services.AddScoped<IRole, Role>();
            services.AddScoped<IAccountRole, AccountRole>();

            services.AddSingleton<IPasswordHandler, PasswordHandler>();
            services.AddSingleton<ITokenHandler, Domain.Handlers.TokenHandler>();

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            return services;
        }
    }
}
