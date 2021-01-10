using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportAPI.Services.Country
{
    using Models;
    
    public interface ICountryService
    {
        public Task GetAllByPattern(string pattern);
    }
}