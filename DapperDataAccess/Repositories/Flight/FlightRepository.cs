using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AirportAPI.Utils;
using Dapper;

namespace AirportAPI.DapperDataAccess.Repositories.Flight
{
    using Models;

    public class FlightRepository : IFlightRepository
    {
        private readonly IDbConnection _dbConnection;

        public FlightRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<Flight> Add(Flight flight)
        {
            const string query =
                @"INSERT INTO flight(trip_id, departure_airport_id, arrive_airport_id, departure_date, arrive_date, flight_code, plane_id, flight_time)
                    VALUES (@TripId, @FromLocationId, @ToLocationId, @DepartureDate, @ArriveDate, @FlightCode, @PlaneId, @FlightTime) RETURNING *";
            return await _dbConnection.QueryFirstAsync<Flight>(query, new
            {
                TripId = flight.Trip.Id,
                DepartureAirportId = flight.DepartureAirport.Id,
                ArriveAirportId = flight.ArriveAirport.Id,
                flight.DepartureDate,
                flight.ArriveDate,
                flight.FlightCode,
                flight.PlaneId,
                flight.FlightTime
            });
        }

        public async Task<List<Flight>> GetAllByQuery(FilterRequest filterRequest)
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var query =
                @"SELECT * FROM flight
                    INNER JOIN airport departure_airport on departure_airport.id = flight.departure_airport_id
                    INNER JOIN country cd on cd.id = departure_airport.country_id
                    INNER JOIN airport arrive_airport on arrive_airport.id = flight.arrive_airport_id
                    INNER JOIN country ca on ca.id = arrive_airport.country_id
                  " + StringUtils.WhereStatementBuilder(filterRequest, "flight");
            
            var result = await ExecuteQuery(query, filterRequest);
            return result.DistinctBy(o => o.Id).ToList();;
        }

        private async Task<IEnumerable<Flight>> ExecuteQuery(string query, object queryParams)
        {
            return await _dbConnection.QueryAsync<
                Flight, Airport, Country, Airport, Country, Flight>(
                query,
                (flight, departureAirport, departureCountry, arriveAirport, arriveCountry) =>
                {
                    flight.DepartureAirport = departureAirport;
                    flight.DepartureAirport.Country = departureCountry;
                    flight.ArriveAirport = arriveAirport;
                    flight.ArriveAirport.Country = arriveCountry;
                    return flight;
                }, queryParams, splitOn: "id,id,id");
        }
    }
}