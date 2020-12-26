using System.Threading.Tasks;
using AirportAPI.Models;
using AirportAPI.Services.Flight.CreateFlight;
using Microsoft.AspNetCore.Mvc;

namespace AirportAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlightController : Controller
    {
        private readonly ICreateFlightService _flightService;
        private readonly CreateFlightPresenter _flightPresenter;
        
        public FlightController(ICreateFlightService flightService, CreateFlightPresenter flightPresenter)
        {
            _flightService = flightService;
            _flightPresenter = flightPresenter;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFlight([FromBody] Flight flight)
        {
            await _flightService.Execute(flight);
            return _flightPresenter.ViewModel;
        }
    }
}