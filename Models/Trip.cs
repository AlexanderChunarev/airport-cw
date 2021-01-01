using System.Collections.Generic;

namespace AirportAPI.Models
{
    public class Trip
    {
        public int Id { get; set; }
        public Airline Airline { get; set; }
        public Airport DepartureAirport { get; set; }
        public Airport ArriveAirport { get; set; }
        public List<Flight> Flights { get; set; }
    }
}