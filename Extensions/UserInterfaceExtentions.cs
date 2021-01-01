using AirportAPI.Services.Flight;
using AirportAPI.Services.Trip;
using Microsoft.Extensions.DependencyInjection;
using IOutputPort = AirportAPI.Services.Flight.IOutputPort;

namespace AirportAPI.Extensions
{
    public static class UserInterfaceV1Extensions
    {
        public static IServiceCollection AddPresenters(this IServiceCollection services)
        {
            services.AddScoped<CreateFlightPresenter, CreateFlightPresenter>();
            services.AddScoped<IOutputPort>(
                x => x.GetRequiredService<CreateFlightPresenter>()
            );
            services.AddScoped<TripPresenter, TripPresenter>();
            services.AddScoped<Services.Trip.IOutputPort>(
                x => x.GetRequiredService<TripPresenter>()
            );

            return services;
        }
    }
}