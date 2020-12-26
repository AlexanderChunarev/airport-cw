using System;

namespace AirportAPI.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public Trip Trip { get; set; }
        public Airport DepartureAirport { get; set; }
        public Airport ArriveAirport { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArriveDate { get; set; }
        public string FlightCode { get; set; }
        public int PlaneId { get; set; }
    }
}