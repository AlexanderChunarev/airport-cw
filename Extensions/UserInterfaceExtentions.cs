using AirportAPI.Services.Flight.CreateFlight;
using Microsoft.Extensions.DependencyInjection;

namespace AirportAPI.Extensions
{
    public static class UserInterfaceV1Extensions
    {
        public static IServiceCollection AddPresenters(this IServiceCollection services)
        {
            services.AddScoped<CreateFlightPresenter, CreateFlightPresenter>();
            services.AddScoped<AirportAPI.Services.Flight.CreateFlight.IOutputPort>(
                x => x.GetRequiredService<CreateFlightPresenter>()
            );
            
            return services;
        }
    }
}
