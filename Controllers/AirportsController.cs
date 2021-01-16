using System.Threading.Tasks;
using AirportAPI.Services.Airport;
using AirportAPI.Services.Country;
using Microsoft.AspNetCore.Mvc;

namespace AirportAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AirportsController : Controller
    {
        private readonly IAirportService _airportService;
        private readonly AirportPresenter _airportPresenter;

        public AirportsController(IAirportService airportService, AirportPresenter airportPresenter)
        {
            _airportService = airportService;
            _airportPresenter = airportPresenter;
        }

        [HttpGet("query")]
        public async Task<IActionResult> GetAirports(string pattern)
        {
            await _airportService.GetAllByPattern(pattern);
            return _airportPresenter.ViewModel;
        }
    }
}