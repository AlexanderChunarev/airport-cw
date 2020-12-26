using Microsoft.AspNetCore.Mvc;

namespace AirportAPI.Services.Trip.GetTrip
{
    public sealed class TripDetailsPresenter : IOutputPort
    {
        public IActionResult ViewModel { get; set; }

        public void Ok(Models.Trip trip) => ViewModel = new JsonResult(trip);
    }
}