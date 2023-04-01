﻿using Locations.API.Models;

namespace Locations.API.Helpers;

public static class LocationHelpers
{
    public const double MAX_SPEED_PER_SECOND = 341.10694;

    public static double CalculateTraveledDistanceInSeconds(Location startingLocation, Location endLocation) 
    {
        var firstLocationDistance = startingLocation.Latitude * (Math.PI / 180);
        var firstLocationNumber = startingLocation.Longitude * (Math.PI / 180);
        var secondLocationDistance = endLocation.Latitude * (Math.PI / 180);
        var secondLocationNumber = endLocation.Longitude * (Math.PI / 180) - firstLocationNumber;
        var locationDistanceSubtraction = Math.Pow(Math.Sin((secondLocationDistance - firstLocationDistance) / 2), 2) +
                 Math.Cos(firstLocationDistance) * Math.Cos(secondLocationDistance) * Math.Pow(Math.Sin(secondLocationNumber / 2), 2);
        return Math.Round(6376500 * (2 * Math.Atan2(Math.Sqrt(locationDistanceSubtraction), Math.Sqrt(1 - locationDistanceSubtraction))), 2);
    }

    public static double CalculateTimeDifferenceInSeconds(this DateTime initialTime, DateTime endTime)
        => Math.Round(endTime.Subtract(initialTime).TotalSeconds, 2);

    public static double CalculateSpeedInMetersPerSecond(this double distanceCovered, double timeSpent)
        => distanceCovered / timeSpent;
}
