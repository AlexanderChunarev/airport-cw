using System.Threading.Tasks;
using AirportAPI.DapperDataAccess.Repositories.Flight;

namespace AirportAPI.Services.Flight.GetFlight
{
    public class GetFlightDetailsService : IGetFlightDetailsService
    {
        private readonly IFlightRepository _flightRepository;
        private readonly IOutputPort _outputPort;

        public GetFlightDetailsService(IFlightRepository flightRepository, IOutputPort outputPort)
        {
            _flightRepository = flightRepository;
            _outputPort = outputPort;
        }

        public async Task Execute(int id)
        {
            var createdFlight = await _flightRepository.GetById(id);
            _outputPort.Ok(createdFlight);
        }
    }
}