using AirportAPI.Services.Flight.CreateFlight;
using AirportAPI.Services.Flight.GetFlight;
using Microsoft.Extensions.DependencyInjection;

namespace AirportAPI.Extensions
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ICreateFlightService, CreateFlightService>();
            services.AddScoped<IGetFlightDetailsService, GetFlightDetailsService>();
            
            return services;
        }
    }
}

