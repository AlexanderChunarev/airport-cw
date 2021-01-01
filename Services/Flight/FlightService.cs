using System.Threading.Tasks;
using AirportAPI.DapperDataAccess.Repositories.Flight;

namespace AirportAPI.Services.Flight
{
    using Models;

    public class FlightService : IFlightService
    {
        private readonly IFlightRepository _flightRepository;
        private readonly IOutputPort _outputPort;

        public FlightService(IFlightRepository flightRepository, IOutputPort outputPort)
        {
            _flightRepository = flightRepository;
            _outputPort = outputPort;
        }

        public async Task Add(Flight flight)
        {
            var createdFlight = await _flightRepository.Add(flight);
            _outputPort.Created(createdFlight);
        }

        public async Task GetById(int id)
        {
            var flight = await _flightRepository.GetById(id);
            _outputPort.Ok(flight);
        }
    }
}