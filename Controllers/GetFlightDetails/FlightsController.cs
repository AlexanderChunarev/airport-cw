using System.Threading.Tasks;
using AirportAPI.Models;
using AirportAPI.Services.Flight.CreateFlight;
using AirportAPI.Services.Flight.GetFlight;
using Microsoft.AspNetCore.Mvc;

namespace AirportAPI.Controllers.GetFlightDetails
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlightsController : Controller
    {
        private readonly IGetFlightDetailsService _flightService;
        private readonly FlightDetailsPresenter _flightPresenter;

        public FlightsController(IGetFlightDetailsService flightService, FlightDetailsPresenter flightPresenter)
        {
            _flightService = flightService;
            _flightPresenter = flightPresenter;
        }

        [HttpGet("{id}/details")]
        public async Task<IActionResult> Get(int id)
        {
            await _flightService.Execute(id);
            return _flightPresenter.ViewModel;
        }
    }
}