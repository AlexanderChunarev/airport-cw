using System;

namespace AirportAPI.Models
{
    public class FilterRequest
    {
        public int DepartureCountryId { get; set; }
        public int ArriveCountryId { get; set; }
        public int DepartureAirportId { get; set; }
        public int ArriveAirportId { get; set; }
        public int AirlineId { get; set; }
        public DateTime DepartureDate { get; set; }
    }
}