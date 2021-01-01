﻿using System.Threading.Tasks;
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
        public async Task<IActionResult> Get(int id)
        {
            await _tripService.GetById(id);
            return _tripPresenter.ViewModel;
        }
    }
}