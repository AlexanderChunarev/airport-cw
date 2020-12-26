using System.Threading.Tasks;
using AirportAPI.DapperDataAccess.Repositories.Trip;

namespace AirportAPI.Services.Trip.GetTrip
{
    public class GetTripDetailsService : IGetTripDetailsService
    {
        private readonly ITripRepository _tripRepository;
        private readonly IOutputPort _outputPort;

        public GetTripDetailsService(ITripRepository tripRepository, IOutputPort outputPort)
        {
            _tripRepository = tripRepository;
            _outputPort = outputPort;
        }

        public async Task Execute(int id)
        {
            var trip = await _tripRepository.GetById(id);
            _outputPort.Ok(trip);
        }
    }
}