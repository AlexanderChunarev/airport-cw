using System.Threading.Tasks;

namespace AirportAPI.Repositories.Flight
{
    using Models;

    public interface IFlightRepository
    {
        Task<Flight> Add(Flight flight);
    }
}