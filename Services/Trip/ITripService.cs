using System.Threading.Tasks;
using AirportAPI.Models;

namespace AirportAPI.Services.Trip
{
    public interface ITripService
    {
        public Task GetById(int id);

        public Task GetByQuery(FilterRequest filterRequest);
    }
}