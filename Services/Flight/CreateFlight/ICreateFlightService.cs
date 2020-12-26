using System.Threading.Tasks;

namespace AirportAPI.Services.Flight.CreateFlight
{
    public interface ICreateFlightService
    {
        public Task Execute(Models.Flight flight);
    }
}