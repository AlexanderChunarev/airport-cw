using System.Threading.Tasks;
using AirportAPI.DapperDataAccess.Repositories.Trip;

namespace AirportAPI.Services.Trip
{
    public class TripService : ITripService
    {
        private readonly ITripRepository _tripRepository;
        private readonly IOutputPort _outputPort;

        public TripService(ITripRepository tripRepository, IOutputPort outputPort)
        {
            _tripRepository = tripRepository;
            _outputPort = outputPort;
        }

        public async Task GetById(int id)
        {
            var trip = await _tripRepository.GetById(id);
            _outputPort.Ok(trip);
        }

        public async Task GetByDestinations(int departureId, int arriveId)
        {
            var trips = await _tripRepository.GetByDestination(departureId, arriveId);
            _outputPort.Ok(trips);
        }
    }
}