using System.Threading.Tasks;
using AirportAPI.Models;
using AirportAPI.Services.Flight.CreateFlight;
using Microsoft.AspNetCore.Mvc;

namespace AirportAPI.Controllers.CreateFlight
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlightsController : Controller
    {
        private readonly ICreateFlightService _flightService;
        private readonly CreateFlightPresenter _flightPresenter;
        
        public FlightsController(ICreateFlightService flightService, CreateFlightPresenter flightPresenter)
        {
            _flightService = flightService;
            _flightPresenter = flightPresenter;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] Flight flight)
        {
            await _flightService.Execute(flight);
            return _flightPresenter.ViewModel;
        }
    }
}