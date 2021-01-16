using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AirportAPI.DapperDataAccess.Repositories.Country;

namespace AirportAPI.Services.Country
{
    using Models;
    
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IOutputPort _outputPort;

        public CountryService(ICountryRepository countryRepository, IOutputPort outputPort)
        {
            _countryRepository = countryRepository;
            _outputPort = outputPort;
        }

        public async Task GetAllByPattern(string pattern)
        {
            var result = new List<Country>();
            if (pattern != null)
            {
                result = await _countryRepository.GetAllByPattern(pattern);
            }
            _outputPort.Ok(result);
        }
    }
}