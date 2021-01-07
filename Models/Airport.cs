namespace AirportAPI.Models
{
    public class Airport
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
