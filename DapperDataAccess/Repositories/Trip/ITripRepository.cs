using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportAPI.DapperDataAccess.Repositories.Trip
{
    using Models;

    public interface ITripRepository
    {
        Task<Trip> GetById(int id);

        Task<List<Trip>> GetByQuery(int departureId, int arriveId, int airlineId);

        Task<List<Trip>> GetAll(int departureId, int arriveId);
    }
}