using Domain.Enums;
using Domain.Exceptions;

namespace Internship_4_OOP2.Domain.ValueObjects
{
    public class GeoLocation
    {
        public double Lat { get; }
        public double Lng { get; }

        public GeoLocation(double lat, double lng)
        {
            if (lat < -90 || lat > 90)
                throw new ValidationException(
                    "Latitude must be between -90 and 90 degrees.",
                    "USR_LAT_INVALID",
                    Severity.Error
                );

            if (lng < -180 || lng > 180)
                throw new ValidationException(
                    "Longitude must be between -180 and 180 degrees.",
                    "USR_LNG_INVALID",
                    Severity.Error
                );

            Lat = lat;
            Lng = lng;
        }

        public double DistanceTo(GeoLocation other)
        {
            const double EarthRadius = 6371.0;

            double dLat = ToRadians((double)(other.Lat - Lat));
            double dLng = ToRadians((double)(other.Lng - Lng));

            double a =
                Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(ToRadians((double)Lat)) * Math.Cos(ToRadians((double)other.Lat)) *
                Math.Sin(dLng / 2) * Math.Sin(dLng / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return EarthRadius * c;
        }

        private double ToRadians(double degrees)
            => degrees * (Math.PI / 180);

        public override string ToString() => $"{Lat}, {Lng}";
    }
}
