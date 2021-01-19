using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AirportAPI.Services.Trip.Boundaries;
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
            const string query = @"SELECT * FROM trip
                       INNER JOIN airline ON airline.id=trip.airline_id
                       INNER JOIN airport da ON da.id = trip.departure_airport_id
                       INNER JOIN country dc ON dc.id = trip.departure_country_id
                       INNER JOIN airport aa ON aa.id = trip.arrive_airport_id
                       INNER JOIN country ac ON ac.id = trip.arrive_country_id
                       WHERE trip.id=@Id";
            var result = await ExecuteQuery(query, new {Id = id});

            return result.DistinctBy(o => o.Id).Last();
        }

        public async Task<List<Trip>> GetByQuery(FilterRequest filterRequest)
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            Console.WriteLine(StringUtils.WhereStatementBuilder(filterRequest, "trip"));
            var query = @"SELECT * FROM trip
                       INNER JOIN airline ON airline.id=trip.airline_id
                       INNER JOIN airport da ON da.id = trip.departure_airport_id
                       INNER JOIN country dc ON dc.id = trip.departure_country_id
                       INNER JOIN airport aa ON aa.id = trip.arrive_airport_id
                       INNER JOIN country ac ON ac.id = trip.arrive_country_id " 
                        + StringUtils.WhereStatementBuilder(filterRequest, "trip");
            var result = await ExecuteQuery(query, filterRequest);

            return result.DistinctBy(o => o.Id).ToList();
        }

        public async Task<int> Add(AddTripInput input)
        {
            const string query =
                @"INSERT INTO trip(airline_id, departure_airport_id, arrive_airport_id, departure_country_id, arrive_country_id, departure_date)
                    VALUES (@AirlineId, @DepartureAirportId, @ArriveAirportId, @DepartureCountryId, @ArriveCountryId, @DepartureDate) RETURNING id";
            return await _dbConnection.QueryFirstAsync<int>(query, input);
        }

        private async Task<IEnumerable<Trip>> ExecuteQuery(string query, object queryParams)
        {
            return await _dbConnection.QueryAsync<
                Trip, Airline, Airport, Country, Airport, Country, Trip>(
                query,
                (trip, airline, departureAirport, departureCountry, arriveAirport, arriveCountry) =>
                {

                    trip.Airline = airline;
                    trip.DepartureAirport = departureAirport;
                    trip.DepartureAirport.Country = departureCountry;
                    trip.ArriveAirport = arriveAirport;
                    trip.ArriveAirport.Country = arriveCountry;
                    
                    return trip;
                }, queryParams, splitOn: "id,id,id,id,id");
        }
    }
}