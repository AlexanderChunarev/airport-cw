using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportAPI.DapperDataAccess.Repositories.Country
{
    using Models;

    public interface ICountryRepository
    {
        Task<List<Country>> GetAllByPattern(string pattern);
    }
}