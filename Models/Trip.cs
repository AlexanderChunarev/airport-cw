using System.Collections.Generic;

namespace AirportAPI.Models
{
    public class Trip
    {
        public int Id { get; set; }
        public Airline Airline { get; set; }
        public List<Flight> Flights { get; set; }
    }
}