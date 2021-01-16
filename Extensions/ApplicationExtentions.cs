using AirportAPI.Services.Airport;
using AirportAPI.Services.Country;
using AirportAPI.Services.Flight;
using AirportAPI.Services.Trip;
using Microsoft.Extensions.DependencyInjection;

namespace AirportAPI.Extensions
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IFlightService, FlightService>();
            services.AddScoped<ITripService, TripService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IAirportService, AirportService>();

            return services;
        }
    }
}