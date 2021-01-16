using System;

namespace AirportAPI.Utils
{
    public static class GeolocationUtils
    {
        private const int EarthRadius = 6371;

        public static double CalculateDistanceByCoordinates(double longitude1, double latitude1, double longitude2,
            double latitude2)
        {
            var degLatitude = (latitude2 - latitude1).ToRadians();
            var degLongitude = (longitude2 - longitude1).ToRadians();
            var a = Math.Sin(degLatitude / 2) * Math.Sin(degLatitude / 2) +
                    Math.Cos(latitude1.ToRadians()) * Math.Cos(latitude2.ToRadians()) *
                    Math.Sin(degLongitude / 2) * Math.Sin(degLongitude / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var distance = EarthRadius * c;

            return distance;
        }

        public static string CalculateFlightTime(double hours)
        {
            var fromHours = TimeSpan.FromHours(hours);
            var time = $"{(int) fromHours.TotalHours}h {fromHours.Minutes}mins";
            return time;
        }

        private static double ToRadians(this double val)
        {
            return (Math.PI / 180) * val;
        }
    }
}