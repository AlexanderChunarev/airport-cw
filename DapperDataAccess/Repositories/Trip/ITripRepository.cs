using System.Collections.Generic;
using System.Threading.Tasks;
using AirportAPI.Services.Trip.Boundaries;

namespace AirportAPI.DapperDataAccess.Repositories.Trip
{
    using Models;

    public interface ITripRepository
    {
        Task<Trip> GetById(int id);

        Task<List<Trip>> GetByQuery(FilterRequest filterRequest);

        Task<int> Add(AddTripInput input);
    }
}