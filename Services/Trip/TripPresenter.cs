using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace AirportAPI.Services.Trip
{
    using Models;

    public sealed class TripPresenter : IOutputPort
    {
        public IActionResult ViewModel { get; set; }

        public void Ok(Trip trip) => ViewModel = new JsonResult(trip);

        public void Ok(List<Trip> trips) => ViewModel = new JsonResult(trips);
    }
}