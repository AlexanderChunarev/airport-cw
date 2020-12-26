using System.Threading.Tasks;
using AirportAPI.DapperDataAccess.Repositories.Flight;

namespace AirportAPI.Services.Flight.CreateFlight
{
    using Models;
    
    public class CreateFlightService : ICreateFlightService
    {
        private readonly IFlightRepository _flightRepository;
        private readonly IOutputPort _outputPort;

        public CreateFlightService(IFlightRepository flightRepository, IOutputPort outputPort)
        {
            _flightRepository = flightRepository;
            _outputPort = outputPort;
        }

        public async Task Execute(Flight flight)
        {
            var createdFlight = await _flightRepository.Add(flight);
            _outputPort.Ok(createdFlight);
        }
    }
}