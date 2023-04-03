namespace Locations.Consoles;

public sealed class CsvLocation
{
    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public int ZeroValue { get; set; }

    public double Altitude { get; set; }

    public double NumberOfDays { get; set; }

    public DateTime Date { get; set; }

    public string Time { get; set; }
}
