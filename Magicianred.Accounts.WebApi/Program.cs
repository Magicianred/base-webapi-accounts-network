using EFContextMsSql = Magicianred.Accounts.DAL.EF.MsSql.Data;
using DapperSql = Magicianred.Accounts.DAL.Dapper.Data;
using Magicianred.Accounts.WebApi.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Magicianred.Accounts.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            var myEnv = System.Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", false)
                .AddJsonFile($"appsettings.{myEnv}.json", false)
                .Build();

            var host = CreateHostBuilder(args).Build();

            var storageType = config.GetValue<string>("StorageType");

            if (config.GetValue<bool>("UseDataSeed"))
            {
                using var scope = host.Services.CreateScope();
                switch (storageType)
                {
                    case StorageTypeEnum.ENTITY_FRAMEWORK_MSSQL:
                        EFContextMsSql.DatabaseSeed.Seed(scope);
                        break;
                    case StorageTypeEnum.DAPPER_MSSQL:
                        DapperSql.DatabaseSeed.Seed(scope);
                        break;
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
