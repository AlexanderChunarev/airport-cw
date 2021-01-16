using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace AirportAPI.Services.Airport
{
    public sealed class AirportPresenter : IOutputPort
    {
        public IActionResult ViewModel { get; set; }

        public void Ok(List<Models.Airport> airports) => ViewModel = new JsonResult(airports);
    }
}
