using AirportAPI.Services.Flight.CreateFlight;
using AirportAPI.Services.Flight.GetFlight;
using AirportAPI.Services.Trip.GetTrip;
using Microsoft.Extensions.DependencyInjection;

namespace AirportAPI.Extensions
{
    public static class UserInterfaceV1Extensions
    {
        public static IServiceCollection AddPresenters(this IServiceCollection services)
        {
            services.AddScoped<CreateFlightPresenter, CreateFlightPresenter>();
            services.AddScoped<Services.Flight.CreateFlight.IOutputPort>(
                x => x.GetRequiredService<CreateFlightPresenter>()
            );
            services.AddScoped<FlightDetailsPresenter, FlightDetailsPresenter>();
            services.AddScoped<Services.Flight.GetFlight.IOutputPort>(
                x => x.GetRequiredService<FlightDetailsPresenter>()
            );
            services.AddScoped<TripDetailsPresenter, TripDetailsPresenter>();
            services.AddScoped<Services.Trip.GetTrip.IOutputPort>(
                x => x.GetRequiredService<TripDetailsPresenter>()
            );

            return services;
        }
    }
}