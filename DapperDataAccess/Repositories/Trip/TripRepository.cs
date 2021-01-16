using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AirportAPI.Utils;
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
            const string query =
                @"SELECT * FROM trip
                       INNER JOIN airline ON airline.id=trip.airline_id
                       INNER JOIN (SELECT * FROM flight
                                      INNER JOIN airport departure_airport ON departure_airport.id = flight.departure_airport_id
                                      INNER JOIN airport arrive_airport ON arrive_airport.id = flight.arrive_airport_id
                                  ) AS flights ON flights.trip_id = trip.id
                  WHERE trip.id=@TripId";
            var result = await ExecuteQuery(query, new {TripId = id});
            return result.FirstOrDefault();
        }

        public async Task<List<Trip>> GetByQuery(FilterRequest filterRequest)
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var query = @"SELECT * FROM trip
                       INNER JOIN airline ON airline.id=trip.airline_id
                       INNER JOIN (SELECT * FROM flight
                                      INNER JOIN airport departure_airport ON departure_airport.id = flight.departure_airport_id
                                      INNER JOIN country departure_country ON departure_country.id = departure_airport.country_id
                                      INNER JOIN airport arrive_airport ON arrive_airport.id = flight.arrive_airport_id
                                      INNER JOIN country arrive_country ON arrive_country.id = arrive_airport.country_id
                                   WHERE flight.departure_date::timestamp < @DepartureDate::timestamp
                                  ) AS flights ON flights.trip_id = trip.id " + WhereStatementBuilder(filterRequest, "trip");
            
            var result = await ExecuteQuery(query, filterRequest);

            return result.DistinctBy(o => o.Id).ToList();
        }

        private async Task<IEnumerable<Trip>> ExecuteQuery(string query, object queryParams)
        {
            var flights = new Dictionary<int, Trip>();

            return await _dbConnection.QueryAsync<Trip, Airline, Flight, Airport, Country, Airport, Country, Trip>(
                query,
                (trip, airline, flight, departureAirport, departureCountry, arriveAirport, arriveCountry) =>
                {
                    if (!flights.TryGetValue(trip.Id, out var tripEntity))
                    {
                        tripEntity = trip;
                        tripEntity.Flights = new List<Flight>();
                        flights.Add(tripEntity.Id, tripEntity);
                        tripEntity.DepartureAirport = departureAirport;
                        tripEntity.ArriveAirport = arriveAirport;
                    }

                    departureAirport.Country = departureCountry;
                    arriveAirport.Country = arriveCountry;
                    flight.DepartureAirport = departureAirport;
                    flight.ArriveAirport = arriveAirport;

                    tripEntity.Flights.Add(flight);
                    tripEntity.Airline = airline;

                    return trip;
                }, queryParams, splitOn: "id,id,id,id,id,id");
        }

        private string WhereStatementBuilder(FilterRequest request, string tableName)
        {
            var segments = request.GetType().GetProperties()
                .Where(prop => prop.PropertyType == typeof(int) && (int) prop.GetValue(request, null) != 0)
                .Select(prop => $"{tableName}.{prop.Name.ToUnderscoreCase()}={prop.GetValue(request)}")
                .ToList();

            return (segments.Any()) ? "WHERE " + string.Join(" AND ", segments) : "";
        }
    }
}