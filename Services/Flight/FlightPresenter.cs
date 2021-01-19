using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace AirportAPI.Services.Flight
{
    using Models;

    public sealed class CreateFlightPresenter : IOutputPort
    {
        public IActionResult ViewModel { get; set; }

        public void Ok(Flight flight) => ViewModel = new JsonResult(flight);
        public void Ok(List<Flight> flights) => ViewModel = new JsonResult(flights);

        public void Created(Flight flight) => ViewModel = new CreatedResult("", flight);
    }
}