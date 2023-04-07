using Locations.API.Helpers;
using Locations.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locations.Tests.Helpers;

public class LocationHelpersTests
{
    [Fact]
    public void CalculateTraveledDistanceInSeconds_ValuesMatchExpected() 
    {
        //var firstLocation = new Location(
        //    Guid.NewGuid().ToString(),
        //    2.5,
        //    3.0);

        //var secondLocation = new Location(
        //    Guid.NewGuid().ToString(),
        //    10.0,
        //    5.2);

        //var act = LocationHelpers.CalculateTraveledDistanceInSeconds(firstLocation, secondLocation);

        //Assert.Equal(869391.90, act);
    }

    [Fact]
    public void CalculateTimeDifferenceInSeconds_TimeDifferenceMatchesExpected() 
    {
        var initialTime = DateTime.UtcNow;
        var endTime = DateTime.UtcNow.AddDays(1);

        var act = initialTime.CalculateTimeDifferenceInSeconds(endTime);

        Assert.Equal(86400, act);
    }

    [Theory]
    [InlineData(500, 20, 25)]
    [InlineData(100, 10, 10)]
    public void CalculateSpeedInMetersPerSecond_MetersPerSecondMatchExpected(double distance, double timeTaken, double expected)
    {
        var act = distance.CalculateSpeedInMetersPerSecond(timeTaken);

        Assert.Equal(expected, act);
    }
}
