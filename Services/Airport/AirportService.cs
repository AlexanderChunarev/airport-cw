using System.Collections.Generic;
using System.Threading.Tasks;
using AirportAPI.DapperDataAccess.Repositories.Airport;

namespace AirportAPI.Services.Airport
{
    public class AirportService : IAirportService
    {
        private readonly IAirportRepository _airportRepository;
        private readonly IOutputPort _outputPort;

        public AirportService(IAirportRepository airportRepository, IOutputPort outputPort)
        {
            _airportRepository = airportRepository;
            _outputPort = outputPort;
        }

        public async Task GetAllByPattern(string pattern)
        {
            var result = new List<Models.Airport>();
            if (pattern != null)
            {
                result = await _airportRepository.GetAllByPattern(pattern);
            }

            _outputPort.Ok(result);
        }
    }
}