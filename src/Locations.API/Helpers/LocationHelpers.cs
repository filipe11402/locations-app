using Locations.API.Models;

namespace Locations.API.Helpers;

public static class LocationHelpers
{
    public const double MAX_SPEED_PER_SECOND = 341.10694;

    public static double CalculateTraveledDistanceInSeconds(Location startingLocation, Location endLocation) 
    {
        var firstLocationDistance = startingLocation.Latitude * (Math.PI / 180.0);
        var firstLocationNumber = startingLocation.Longitude * (Math.PI / 180.0);
        var secondLocationDistance = endLocation.Latitude * (Math.PI / 180.0);
        var secondLocationNumber = endLocation.Longitude * (Math.PI / 180.0) - firstLocationNumber;
        var locationDistanceSubtraction = Math.Pow(Math.Sin((secondLocationDistance - firstLocationDistance) / 2.0), 2.0) +
                 Math.Cos(firstLocationDistance) * Math.Cos(secondLocationDistance) * Math.Pow(Math.Sin(secondLocationNumber / 2.0), 2.0);
        return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(locationDistanceSubtraction), Math.Sqrt(1.0 - locationDistanceSubtraction)));
    }

    public static int CalculateTimeDifferenceInSeconds(this DateTime initialTime, DateTime endTime)
        => (endTime - initialTime).Seconds;

    public static double CalculateSpeedInMetersPerSecond(this double distanceCovered, int timeSpent)
        => distanceCovered / timeSpent;
}
