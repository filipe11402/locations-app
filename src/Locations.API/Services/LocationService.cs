using CsvHelper;
using Locations.API.Models;
using Locations.API.Requests;
using System.Globalization;

namespace Locations.API.Services;

public interface ILocationService 
{
    Task<bool> SaveLocationAsync(SaveLocationRequest request);

    Task<MemoryStream> DownloadLocationFileAsync(string deviceId);
}

public class LocationService : ILocationService
{
    private readonly ILogger<LocationService> _logger;

    public LocationService(ILogger<LocationService> logger)
    {
        _logger = logger;
    }

    public Task<MemoryStream?> DownloadLocationFileAsync(string deviceId)
    {
        try
        {
            using var streamWriter = new StreamReader($"{Directory.GetCurrentDirectory()}/Locations/{deviceId}.csv");
            using var csvWriter = new CsvReader(streamWriter, CultureInfo.InvariantCulture);

            var memoryStream = new MemoryStream();

            streamWriter.BaseStream.CopyTo(memoryStream);

            csvWriter.Dispose();
            streamWriter.Dispose();
            memoryStream.Dispose();

            return Task.FromResult(memoryStream)!;
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"File was not found for deviceId: {deviceId}\n{ex.Message}");

            return Task.FromResult(null as MemoryStream);
        }
    }

    public async Task<bool> SaveLocationAsync(SaveLocationRequest location) 
    {
        using var streamWriter = new StreamWriter($"{Directory.GetCurrentDirectory()}/Locations/{location.DeviceId}.csv");
        using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

        csvWriter.WriteRecord(
            new Location
            {
                //TODO: check this scenario
                //DeviceId = location.DeviceId,
                Latitude = location.Latitude,
                Longitude = location.Longitude
            });

        await csvWriter.DisposeAsync();
        await streamWriter.DisposeAsync();

        return true;
    }
}
