namespace Locations.API.Requests;

public record SaveLocationRequest(string Latitude, string Longitude, string DeviceId);