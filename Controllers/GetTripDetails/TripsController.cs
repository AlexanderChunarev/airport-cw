using System.Threading.Tasks;
using AirportAPI.Services.Trip.GetTrip;
using Microsoft.AspNetCore.Mvc;

namespace AirportAPI.Controllers.GetTripDetails
{
    [ApiController]
    [Route("api/[controller]")]
    public class TripsController : Controller
    {
        private readonly IGetTripDetailsService _tripService;
        private readonly TripDetailsPresenter _tripPresenter;

        public TripsController(IGetTripDetailsService tripService, TripDetailsPresenter tripPresenter)
        {
            _tripService = tripService;
            _tripPresenter = tripPresenter;
        }

        [HttpGet("{id}/details")]
        public async Task<IActionResult> Get(int id)
        {
            await _tripService.Execute(id);
            return _tripPresenter.ViewModel;
        }
    }
}