using Magicianred.Accounts.DAL.Dapper.Extensions;
using Magicianred.Accounts.DAL.Dapper.MsSql.Extensions;
using Magicianred.Accounts.DAL.Dapper.MySql.Extensions;
using Magicianred.Accounts.DAL.EF.MsSql.Extensions;
using Magicianred.Accounts.Domain.Extensions;
using Magicianred.Accounts.Domain.Interfaces.Services.Communication;
using Magicianred.Accounts.Domain.Models;
using Magicianred.Accounts.Domain.Services.Communication;
using Magicianred.Accounts.WebApi.Enums;
using Magicianred.Accounts.WebApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Magicianred.Accounts.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddBaseDomain();

            // fix https://stackoverflow.com/questions/49824690/unable-to-resolve-service-for-type-system-string-while-attempting-to-activate
            services.AddScoped<ICreateUserResponse>(_ => new CreateUserResponse(true, "", new Account()));
            //services.AddScoped<ICreateUserResponse, CreateUserResponse>();

            var useInMemoryStore = Configuration.GetValue<bool>("UseInMemoryStore");
            var storageType = Configuration.GetValue<string>("StorageType");

            switch (storageType.ToLower())
            {
                case StorageTypeEnum.ENTITY_FRAMEWORK_MSSQL:
                    services.AddContextSupport(Configuration.GetConnectionString("MyDatabase"), useInMemoryStore);
                    break;
                case StorageTypeEnum.DAPPER_MSSQL:
                case StorageTypeEnum.DAPPER_MYSQL:
                    services.AddContextSupport();
                    if (storageType.ToLower() == StorageTypeEnum.DAPPER_MSSQL)
                        services.AddMsSqlContextSupport();
                    if (storageType.ToLower() == StorageTypeEnum.DAPPER_MYSQL)
                        services.AddMySqlContextSupport();
                    break;
                default:
                    throw new ArgumentException("Startup.StorageType.Not");
                    break;
            }

            services.AddJwtTokenSetup(Configuration);
            services.AddControllers();
            services.AddCors();

            services.AddCustomSwagger();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCustomSwagger();

            app.UseCors(option => option
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
