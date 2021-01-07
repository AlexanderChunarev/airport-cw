using System.Threading.Tasks;

namespace AirportAPI.Services.Trip
{
    public interface ITripService
    {
        public Task GetById(int id);

        public Task GetByQuery(int departureId, int arriveId, int airlineId);
        
        public Task GetAll(int departureId, int arriveId);
    }
}