namespace Locations.API.Models;

public class Location
{
	public string DeviceId { get; set; }

    public double Latitude { get; set; }

	public double Longitude { get; set; }

    public Location(
        string deviceId,
        double latitude,
        double longitude)
    {
        DeviceId = deviceId;
        Latitude = latitude;
        Longitude = longitude;
    }
}
