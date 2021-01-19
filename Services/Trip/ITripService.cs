using System.Threading.Tasks;
using AirportAPI.Services.Trip.Boundaries;

namespace AirportAPI.Services.Trip
{
    using AirportAPI.Models;

    public interface ITripService
    {
        public Task GetById(int id);

        public Task GetByQuery(FilterRequest filterRequest);

        public Task Add(AddTripInput input);
    }
}