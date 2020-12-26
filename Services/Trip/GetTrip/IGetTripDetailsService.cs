using System.Threading.Tasks;

namespace AirportAPI.Services.Trip.GetTrip
{
    public interface IGetTripDetailsService
    {
        public Task Execute(int id);
    }
}