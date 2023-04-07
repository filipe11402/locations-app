namespace Locations.Consoles;

public class Location
{
    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public double Altitude { get; set; }

    public int ZeroValue { get; set; }

    public double NumberOfDays { get; set; }

    public string Date { get; set; }

    public string Time { get; set; }

    public Location()
    {
    }

    public Location(
        double latitude,
        double longitude,
        double altitude,
        int zeroValue,
        double numberOfDays,
        string date,
        string time)
    {
        Latitude = latitude;
        Longitude = longitude;
        Altitude = altitude;
        ZeroValue = zeroValue;
        NumberOfDays = numberOfDays;
        Date = date;
        Time = time;
    }
}
