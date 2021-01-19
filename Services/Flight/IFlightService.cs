using System.Threading.Tasks;

namespace AirportAPI.Services.Flight
{
    using Models;

    public interface IFlightService
    {
        public Task Add(Flight flight);

        public Task GetAllByTripId(FilterRequest filterRequest);
    }
}