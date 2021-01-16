using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirportAPI.DapperDataAccess.Repositories.Airport
{
    using Models;
    
    public interface IAirportRepository
    {
        Task<Airport> GetById(int id);
        
        Task<List<Airport>> GetAllByPattern(string pattern);
    }
}