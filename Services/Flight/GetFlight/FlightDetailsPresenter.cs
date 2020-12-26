using Microsoft.AspNetCore.Mvc;

namespace AirportAPI.Services.Flight.GetFlight
{
    public sealed class FlightDetailsPresenter : IOutputPort
    {
        public IActionResult ViewModel { get; set; }

        public void Ok(Models.Flight flight) => ViewModel = new JsonResult(flight);
    }
}