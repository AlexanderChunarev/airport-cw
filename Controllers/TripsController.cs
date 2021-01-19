using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AirportAPI.Models;
using AirportAPI.Services.Trip;
using AirportAPI.Services.Trip.Boundaries;
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

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] AddTripInput input)
        {
            await _tripService.Add(input);
            return _tripPresenter.ViewModel;
        }

        [HttpGet("{id}/details")]
        public async Task<IActionResult> GetTrip(int id)
        {
            await _tripService.GetById(id);
            return _tripPresenter.ViewModel;
        }

        [HttpGet("query")]
        public async Task<IActionResult> Query([FromQuery] FilterRequest filterRequest)
        {
            await _tripService.GetByQuery(filterRequest);
            return _tripPresenter.ViewModel;
        }
    }
}