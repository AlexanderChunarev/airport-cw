using AirportAPI.DapperDataAccess.Migrations;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AirportAPI.Extensions
{
    public static class MigrationExtension
    {
        public static IServiceCollection AddMigration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddFluentMigratorCore()
                .ConfigureRunner( builder => 
                    builder.AddPostgres()
                        .WithGlobalConnectionString(configuration.GetConnectionString("DefaultConnection"))
                        .ScanIn(typeof(Migration_202018122102).Assembly).For.All()
                ).BuildServiceProvider();
            return services;
        }
        public static IApplicationBuilder Migrate(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var runner = scope.ServiceProvider.GetService<IMigrationRunner>();
            runner.ListMigrations();
            runner.MigrateUp();
            return app;
        }
    }
}

