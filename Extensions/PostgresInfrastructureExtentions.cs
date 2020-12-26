using System.Data;
using AirportAPI.DapperDataAccess.Repositories.Flight;
using AirportAPI.DapperDataAccess.Repositories.Trip;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace AirportAPI.Extensions
{
    public static class PostgresInfrastructureExtensions
    {
        public static IServiceCollection AddPostgresPersistence(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddTransient<IDbConnection>(
                options => new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IFlightRepository, FlightRepository>();
            services.AddScoped<ITripRepository, TripRepository>();
            
            return services;
        }
    }
}