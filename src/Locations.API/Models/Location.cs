namespace Locations.API.Models;

public class Location
{
	public string DeviceId { get; set; }

    public string Latitude { get; set; }

	public string Longitude { get; set; }

    public Location(
        string deviceId,
        string latitude,
        string longitude)
    {
        DeviceId = deviceId;
        Latitude = latitude;
        Longitude = longitude;
    }
}
