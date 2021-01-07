using System;
using System.Threading.Tasks;
using AirportAPI.DapperDataAccess.Repositories.Airport;
using AirportAPI.DapperDataAccess.Repositories.Flight;
using AirportAPI.Utils;

namespace AirportAPI.Services.Flight
{
    using Models;

    public class FlightService : IFlightService
    {
        private readonly IFlightRepository _flightRepository;
        private readonly IAirportRepository _airportRepository;
        private readonly IOutputPort _outputPort;

        public FlightService(IFlightRepository flightRepository, IAirportRepository airportRepository,
            IOutputPort outputPort)
        {
            _flightRepository = flightRepository;
            _airportRepository = airportRepository;
            _outputPort = outputPort;
        }

        public async Task Add(Flight flight)
        {
            var departureAirport = await _airportRepository.GetById(flight.DepartureAirport.Id);
            var arriveAirport = await _airportRepository.GetById(flight.ArriveAirport.Id);
            
            var distance = GeolocationUtils.CalculateDistanceByCoordinates(
                departureAirport.Longitude,
                departureAirport.Latitude,
                arriveAirport.Longitude,
                arriveAirport.Latitude
            );

            var hours = distance / 840;
            flight.ArriveDate = flight.DepartureDate.AddHours(hours);
            flight.FlightTime = hours;
            
            var createdFlight = await _flightRepository.Add(flight);
            _outputPort.Created(createdFlight);
        }

        public async Task GetById(int id)
        {
            var flight = await _flightRepository.GetById(id);
            _outputPort.Ok(flight);
        }

        private DateTime CalculateArriveDateTimeByDistance(double distance, DateTime departureDate)
        {
            const int averagePlaneSpeed = 840; // km/h
            var date = departureDate.AddHours(distance / averagePlaneSpeed);
            return date;
        }
    }
}