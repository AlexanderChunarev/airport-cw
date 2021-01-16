using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirportAPI.DapperDataAccess.Repositories.Trip;
using AirportAPI.Models;
using AirportAPI.Utils;

namespace AirportAPI.Services.Trip
{
    using Models;
    
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

        public async Task GetByQuery(FilterRequest filterRequest)
        {
            var result = await _tripRepository.GetByQuery(filterRequest);
            var trips = result.Select(trip =>
            {
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