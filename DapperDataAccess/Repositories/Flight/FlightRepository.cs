using System.Data;
using System.Linq;
using System.Threading.Tasks;
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
                @"INSERT INTO flight(trip_id, from_location_id, to_location_id, departure_date, arrive_date, flight_code, plane_id)
                    VALUES (@TripId, @FromLocationId, @ToLocationId, @DepartureDate, @ArriveDate, @FlightCode, @PlaneId) RETURNING *";
            return await _dbConnection.QueryFirstAsync<Flight>(query, new
            {
                TripId = flight.Trip.Id,
                FromLocationId = flight.DepartureAirport.Id,
                ToLocationId = flight.ArriveAirport.Id,
                flight.DepartureDate,
                flight.ArriveDate,
                flight.FlightCode,
                flight.PlaneId
            });
        }

        public async Task<Flight> GetById(int id)
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            const string query =
                @"SELECT *, airline_id FROM flight AS f
                    INNER JOIN trip t on t.id = f.trip_id
                    INNER JOIN airline al on al.id = airline_id
                    INNER JOIN airport a1 on a1.id = f.from_location_id
                    INNER JOIN airport a2 on a2.id = f.to_location_id
                  WHERE f.id=@Id";

            var result = await _dbConnection.QueryAsync<Flight, Trip, Airline, Airport, Airport, Flight>(query,
                (flight, trip, airline, airport, a2) =>
                {
                    flight.Trip = trip;
                    flight.Trip.Airline = airline;
                    flight.DepartureAirport = airport;
                    flight.ArriveAirport = a2;
                    return flight;
                }, param: new {Id = id});

            return result.FirstOrDefault();
        }
    }
}