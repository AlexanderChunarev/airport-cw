using System;
using System.Collections.Generic;

namespace AirportAPI.Models
{
    public class Trip
    {
        public int Id { get; set; }
        public DateTime DepartureDate { get; set; }
        public Airline Airline { get; set; }
        public Airport DepartureAirport { get; set; }
        public Airport ArriveAirport { get; set; }
        public List<Flight> Flights { get; set; }
        public int Transfers { get; set; }
        public int Distance { get; set; }
        public string TotalFlightTime { get; set; }
    }
}