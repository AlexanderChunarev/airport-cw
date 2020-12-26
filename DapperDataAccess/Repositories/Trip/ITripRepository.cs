using System.Threading.Tasks;

namespace AirportAPI.DapperDataAccess.Repositories.Trip
{
    using Models;

    public interface ITripRepository
    {
        Task<Trip> GetById(int id);
    }
}