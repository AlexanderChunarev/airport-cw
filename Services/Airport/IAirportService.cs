using System.Threading.Tasks;

namespace AirportAPI.Services.Airport
{
    public interface IAirportService
    {
        public Task GetAllByPattern(string pattern);
    }
}