using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace AirportAPI.Services.Country
{
    public sealed class CountryPresenter : IOutputPort
    {
        public IActionResult ViewModel { get; set; }

        public void Ok(List<Models.Country> countries) => ViewModel = new JsonResult(countries);
    }
}
