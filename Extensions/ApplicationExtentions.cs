using Microsoft.Extensions.DependencyInjection;

namespace AirportAPI.Extensions
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services;
        }
    }
}

