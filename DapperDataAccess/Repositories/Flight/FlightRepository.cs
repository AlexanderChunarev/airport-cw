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

        public async Task<Flight> GetById(int id)
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            const string query =
                @"SELECT *, airline_id FROM flight AS f
                    INNER JOIN trip t on t.id = f.trip_id
                    INNER JOIN airline a on a.id = airline_id
                    INNER JOIN airport departure_airport on departure_airport.id = f.departure_airport_id
                    INNER JOIN airport arrive_airport on arrive_airport.id = f.arrive_airport_id
                  WHERE f.id=@Id";

            var result = await _dbConnection.QueryAsync<Flight, Trip, Airline, Airport, Airport, Flight>(query,
                (flight, trip, airline, airport, a2) =>
                {
                    flight.Trip = trip;
                    flight.Trip.Airline = airline;
                    flight.DepartureAirport = airport;
                    flight.ArriveAirport = a2;
                    return flight;
                }, new {Id = id});

            return result.FirstOrDefault();
        }
    }
}