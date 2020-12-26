using System.Threading.Tasks;

namespace AirportAPI.DapperDataAccess.Repositories.Flight
{
    public interface IFlightRepository
    {
        Task<Models.Flight> Add(Models.Flight flight);
    }
}