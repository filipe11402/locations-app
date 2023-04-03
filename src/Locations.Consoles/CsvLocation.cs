namespace Locations.Consoles;

public sealed class CsvLocation
{
    //TODO: encapsulation
    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public int ZeroValue { get; set; }

    public double Altitude { get; set; }

    public double NumberOfDays { get; set; }

    public DateTime Date { get; set; }

    public string Time { get; set; }

    public CsvLocation(
        double latitude,
        double longitude, 
        int zeroValue, 
        double altitude, 
        double numberOfDays,
        DateTime date, 
        string time)
    {
        Latitude = latitude;
        Longitude = longitude;
        ZeroValue = zeroValue;
        Altitude = altitude;
        NumberOfDays = numberOfDays;
        Date = date;
        Time = time;
    }
}
