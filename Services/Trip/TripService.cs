using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AirportAPI.DapperDataAccess.Repositories.Flight;
using AirportAPI.DapperDataAccess.Repositories.Trip;
using AirportAPI.Models;
using AirportAPI.Services.Trip.Boundaries;
using AirportAPI.Utils;

namespace AirportAPI.Services.Trip
{
    public class TripService : ITripService
    {
        private readonly ITripRepository _tripRepository;
        private readonly IFlightRepository _flightRepository;
        private readonly IOutputPort _outputPort;

        public TripService(ITripRepository tripRepository, IFlightRepository flightRepository, IOutputPort outputPort)
        {
            _tripRepository = tripRepository;
            _outputPort = outputPort;
            _flightRepository = flightRepository;
        }

        public async Task GetById(int id)
        {
            var trip = await _tripRepository.GetById(id);
            var flights = await _flightRepository.GetAllByQuery(new FilterRequest()
            {
                TripId = id
            });
            trip.Flights = flights;
            trip.Transfers = flights.Count - 1;
            trip.Distance = (int) flights.Select(f => GeolocationUtils.CalculateDistanceByCoordinates(
                f.DepartureAirport.Longitude,
                f.DepartureAirport.Latitude,
                f.ArriveAirport.Longitude,
                f.ArriveAirport.Latitude)).Sum();
            _outputPort.Ok(trip);
        }

        public async Task GetByQuery(FilterRequest filterRequest)
        {
            var result = await _tripRepository.GetByQuery(filterRequest);

            result.ForEach(trip =>
            {
                var filter = new FilterRequest
                {
                    TripId = trip.Id,
                };
                var flights = _flightRepository.GetAllByQuery(filter);
                trip.Flights = flights.Result;
                trip.Transfers = trip.Flights.Count - 1;
                trip.Distance = (int) trip.Flights.Select(f => GeolocationUtils.CalculateDistanceByCoordinates(
                    f.DepartureAirport.Longitude,
                    f.DepartureAirport.Latitude,
                    f.ArriveAirport.Longitude,
                    f.ArriveAirport.Latitude)).Sum();
                trip.TotalFlightTime =
                    GeolocationUtils.CalculateFlightTime(trip.Flights.Select(t => t.FlightTime).Sum());
            });

            _outputPort.Ok(result);
        }

        public async Task Add(AddTripInput input)
        {
            var result = await _tripRepository.Add(input);
            var trip = await _tripRepository.GetById(id: result);

            _outputPort.Ok(trip);
        }
    }
}