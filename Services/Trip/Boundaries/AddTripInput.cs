using System;

namespace AirportAPI.Services.Trip.Boundaries
{
    public class AddTripInput
    {
        public int AirlineId { get; set; }
        public int DepartureAirportId { get; set; }
        public int ArriveAirportId { get; set; }
        public int DepartureCountryId { get; set; }
        public int ArriveCountryId { get; set; }
        public DateTime DepartureDate { get; set; }
    }
}