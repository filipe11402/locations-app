using CsvHelper;
using Locations.API.Models;
using Locations.API.Requests;
using System.Globalization;

namespace Locations.API.Services;

public interface ILocationService 
{
    Task<bool> SaveLocationAsync(SaveLocationRequest request);
}

public class LocationService : ILocationService
{
    public async Task<bool> SaveLocationAsync(SaveLocationRequest location) 
    {
        using var streamWriter = new StreamWriter($"{Directory.GetCurrentDirectory()}/Locations/{location.DeviceId}.csv");
        using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

        csvWriter.WriteRecord(
            new Location(
                location.DeviceId,
                location.Latitude,
                location.Longitude));

        await csvWriter.DisposeAsync();
        await streamWriter.DisposeAsync();

        return true;
    }
}
