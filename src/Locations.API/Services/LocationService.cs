using CsvHelper;
using CsvHelper.Configuration;
using Locations.API.Models;
using Locations.API.Requests;
using System.Formats.Asn1;
using System.Globalization;
using System.Reflection;

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
        try
        {
            var filePath = Path.Combine(
                Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!,
                $"Locations/{location.DeviceId}.csv");

            var csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false
            };

            using var stream = File.Open(filePath, FileMode.Append);

            using var streamWriter = new StreamWriter(stream);
            using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

            csvWriter.WriteRecord(
                new Location(location.DeviceId,
                location.Latitude,
                location.Longitude));
            csvWriter.NextRecord();

            await csvWriter.DisposeAsync();
            await streamWriter.DisposeAsync();
            await stream.DisposeAsync();

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }


    }
}
