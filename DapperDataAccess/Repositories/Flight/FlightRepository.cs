using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace AirportAPI.DapperDataAccess.Repositories.Flight
{
    public class FlightRepository : IFlightRepository
    {
        private readonly IDbConnection _dbConnection;

        public FlightRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<Models.Flight> Add(Models.Flight flight)
        {
            const string query = "INSERT INTO flight(trip_id, from_location_id, to_location_id, departure_date, arrive_date, flight_code, plane_id) VALUES (@TripId, @FromLocationId, @ToLocationId, @DepartureDate, @ArriveDate, @FlightCode, @PlaneId) RETURNING *";
            return await _dbConnection.QueryFirstAsync<Models.Flight>(query, new
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
    }
}