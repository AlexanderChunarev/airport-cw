using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AirportAPI.Services.Trip;
using Microsoft.AspNetCore.Mvc;

namespace AirportAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TripsController : Controller
    {
        private readonly ITripService _tripService;
        private readonly TripPresenter _tripPresenter;

        public TripsController(ITripService tripService, TripPresenter tripPresenter)
        {
            _tripService = tripService;
            _tripPresenter = tripPresenter;
        }

        [HttpGet("{id}/details")]
        public async Task<IActionResult> GetTrip(int id)
        {
            await _tripService.GetById(id);
            return _tripPresenter.ViewModel;
        }
        
        [HttpGet("")]
        public async Task<IActionResult> GetAllTrips(
            [Required] int arriveId,
            [Required] int departureId)
        {
            await _tripService.GetAll(departureId, arriveId);
            return _tripPresenter.ViewModel;
        }

        [HttpGet("filter")]
        public async Task<IActionResult> Query(
            [Required] int arriveId,
            [Required] int departureId,
            [Required] int airlineId)
        {
            await _tripService.GetByQuery(departureId, arriveId, airlineId);
            return _tripPresenter.ViewModel;
        }
    }
}