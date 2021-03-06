﻿using System.Threading.Tasks;
using AirportAPI.Models;
using AirportAPI.Services.Flight;
using Microsoft.AspNetCore.Mvc;

namespace AirportAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlightsController : Controller
    {
        private readonly IFlightService _flightService;
        private readonly CreateFlightPresenter _flightPresenter;

        public FlightsController(IFlightService flightService, CreateFlightPresenter flightPresenter)
        {
            _flightService = flightService;
            _flightPresenter = flightPresenter;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] Flight flight)
        {
            await _flightService.Add(flight);
            return _flightPresenter.ViewModel;
        }

        [HttpGet("query")]
        public async Task<IActionResult> Get([FromQuery] FilterRequest filterRequest)
        {
            await _flightService.GetAllByTripId(filterRequest);
            return _flightPresenter.ViewModel;
        }
    }
}