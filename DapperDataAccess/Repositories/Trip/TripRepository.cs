using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace AirportAPI.DapperDataAccess.Repositories.Trip
{
    using Models;

    public class TripRepository : ITripRepository
    {
        private readonly IDbConnection _dbConnection;

        public TripRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<Trip> GetById(int id)
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            Console.WriteLine(id);
            const string query =
                @"SELECT * FROM trip
                       INNER JOIN airline ON airline.id=trip.airline_id
                       INNER JOIN (SELECT * FROM flight
                                      INNER JOIN airport departure_airport ON departure_airport.id = flight.from_location_id
                                      INNER JOIN airport arrive_airport ON arrive_airport.id = flight.to_location_id
                                  ) AS flights ON flights.trip_id = trip.id
                  WHERE trip.id=@Id";
            var flights = new Dictionary<int, Trip>();
            var result =
                await _dbConnection.QueryAsync<Trip, Airline, Flight, Airport, Airport, Trip>(query,
                    (trip, airline, flight, departureAirport, arriveAirport) =>
                    {
                        if (!flights.TryGetValue(trip.Id, out var tripEntity))
                        {
                            tripEntity = trip;
                            tripEntity.Flights = new List<Flight>();
                            flights.Add(tripEntity.Id, tripEntity);
                        }
                        
                        flight.DepartureAirport = departureAirport;
                        flight.ArriveAirport = arriveAirport;
                        tripEntity.Flights.Add(flight);
                        tripEntity.Airline = airline;

                        return trip;
                    }, new {Id = id}, splitOn: "id,id,id,id");

            return result.FirstOrDefault();
        }
    }
}