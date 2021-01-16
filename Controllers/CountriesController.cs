using System.Threading.Tasks;
using AirportAPI.Services.Country;
using Microsoft.AspNetCore.Mvc;

namespace AirportAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountriesController : Controller
    {
        private readonly ICountryService _countryService;
        private readonly CountryPresenter _countryPresenter;

        public CountriesController(ICountryService countryService, CountryPresenter countryPresenter)
        {
            _countryService = countryService;
            _countryPresenter = countryPresenter;
        }

        [HttpGet("query")]
        public async Task<IActionResult> GetCountries(string pattern)
        {
            await _countryService.GetAllByPattern(pattern);
            return _countryPresenter.ViewModel;
        }
    }
}