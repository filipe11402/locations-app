namespace Locations.API.Models;

public class Location
{
    //public string DeviceId { get; set; }

    public double Latitude { get; set; }

	public double Longitude { get; set; }

    public int ZeroValue { get; set; }

    public double Altitude { get; set; }

    public double NumberOfDays { get; set; }

    public string Date { get; set; }

    public string Time { get; set; }

    public Location()
    {
    }

    //public Location(
    //    string deviceId,
    //    double latitude,
    //    double longitude)
    //{
    //    //DeviceId = deviceId;
    //    Latitude = latitude;
    //    Longitude = longitude;
    //}

    //public Location(
    //    double latitude,
    //    double longitude,
    //    int zeroValue,
    //    double altitude,
    //    double numberOfDays,
    //    DateTime date, 
    //    string time)
    //{
    //    Latitude = latitude;
    //    Longitude = longitude;
    //    ZeroValue = zeroValue;
    //    Altitude = altitude;
    //    NumberOfDays = numberOfDays;
    //    Date = date;
    //    Time = time;
    //}
}
