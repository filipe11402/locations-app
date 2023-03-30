namespace Locations.API.Requests;

public record SaveLocationRequest(double Latitude, double Longitude, string DeviceId);