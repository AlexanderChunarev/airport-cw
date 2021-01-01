using System.Threading.Tasks;

namespace AirportAPI.Services.Trip
{
    public interface ITripService
    {
        public Task GetById(int id);
        
        public Task GetByDestinations(int departureId, int arriveId);
    }
}