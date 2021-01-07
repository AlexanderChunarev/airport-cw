using System;
using System.Linq;
using System.Threading.Tasks;
using AirportAPI.DapperDataAccess.Repositories.Trip;
using AirportAPI.Utils;

namespace AirportAPI.Services.Trip
{
    public class TripService : ITripService
    {
        private readonly ITripRepository _tripRepository;
        private readonly IOutputPort _outputPort;

        public TripService(ITripRepository tripRepository, IOutputPort outputPort)
        {
            _tripRepository = tripRepository;
            _outputPort = outputPort;
        }

        public async Task GetById(int id)
        {
            var trip = await _tripRepository.GetById(id);
            _outputPort.Ok(trip);
        }

        public async Task GetByQuery(int departureId, int arriveId, int airlineId)
        {
            var trips = await _tripRepository.GetByQuery(departureId, arriveId, airlineId);
            _outputPort.Ok(trips);
        }

        public async Task GetAll(int departureId, int arriveId)
        {
            var result = await _tripRepository.GetAll(departureId, arriveId);

            var trips = result.Select(trip =>
            {
                trip.DepartureAirport =
                    trip.Flights.First(f => f.DepartureAirport.Id.Equals(departureId))?.DepartureAirport;
                trip.ArriveAirport =
                    trip.Flights.First(f => f.ArriveAirport.Id.Equals(arriveId))?.ArriveAirport;
                trip.Transfers = trip.Flights.Count - 1;
                trip.Distance = (int) trip.Flights.Select(f => GeolocationUtils.CalculateDistanceByCoordinates(
                    f.DepartureAirport.Longitude,
                    f.DepartureAirport.Latitude,
                    f.ArriveAirport.Longitude,
                    f.ArriveAirport.Latitude)).Sum();
                trip.TotalFlightTime = GeolocationUtils.CalculateFlightTime(
                    trip.Flights.Select(f => f.FlightTime).Sum()
                );

                return trip;
            }).ToList();

            _outputPort.Ok(trips);
        }
    }
}