using System.Threading.Tasks;

namespace AirportAPI.Services.Flight.GetFlight
{
    public interface IGetFlightDetailsService
    {
        public Task Execute(int id);
    }
}