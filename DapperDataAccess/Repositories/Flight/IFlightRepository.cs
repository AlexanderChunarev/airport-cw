using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportAPI.DapperDataAccess.Repositories.Flight
{
    using Models;

    public interface IFlightRepository
    {
        Task<Flight> Add(Flight flight);

        Task<List<Flight>> GetAllByQuery(FilterRequest filterRequest);
    }
}