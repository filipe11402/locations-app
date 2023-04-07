using CsvHelper;
using CsvHelper.Configuration;
using Locations.API.Helpers;
using Locations.Consoles;
using System.Globalization;
using System.Text;

Console.WriteLine("#########################################\n");

Console.WriteLine(@"Introduce the complete path for the file you want to analyze(eg: c:\...): ");
var filePath = Console.ReadLine();

Console.WriteLine(@"Introduce how many lines you would like to skip: ");
int.TryParse(Console.ReadLine(), out int totalLinesToSkip);

Console.WriteLine("\n#########################################\n");

try
{
    using var streamReader = new StreamReader(filePath + ".csv"!);

    var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
    {
        Encoding = Encoding.UTF8,
        Delimiter = ","
    };
    using var csvReader = new CsvReader(streamReader, configuration);

    int locationPoint = 1;

    var locations = FilterCsvRecords(csvReader, totalLinesToSkip);

    var totalTraveledDistance = 0.0;

    var totalSpentTime = 0.0;

    for (int totalLocation = 0; totalLocation < locations.Count(); totalLocation++)
    {
        if (totalLocation + 1 == locations.Count()) { break; }

        var startLocation = locations.ElementAt(totalLocation);
        var endLocation = locations.ElementAt(totalLocation + 1);

        var startLocationCount = 1;
        var endLocationCount = startLocationCount++;

        Console.WriteLine($"PONTO {startLocationCount} VS PONTO {endLocationCount}\n");

        var initialTime = DateTime.Parse($"{startLocation.Date} {startLocation.Time}");
        var endTime = DateTime.Parse($"{endLocation.Date} {endLocation.Time}");
        var timeDifferece = initialTime.CalculateTimeDifferenceInSeconds(endTime);

        totalSpentTime += timeDifferece;

        Console.WriteLine($"TEMPO DECORRIDO: {timeDifferece}s \n");

        var traveledDistance = LocationHelpers.CalculateTraveledDistanceInSeconds(startLocation, endLocation);

        totalTraveledDistance += traveledDistance;

        Console.WriteLine($"DISTANCIA ENTRE PONTO {startLocationCount} VS PONTO {endLocationCount}: {traveledDistance}m \n");

        var transportationSpeed = traveledDistance.CalculateSpeedInMetersPerSecond(timeDifferece);

        Console.WriteLine($"VELOCIDADE ENTRE PONTO {startLocationCount} VS PONTO {endLocationCount}: {transportationSpeed} m/s \n");

        var transportationSpeedInKmsPerHour = transportationSpeed.CalculateSpeedInKmPerHour();

        Console.WriteLine($"VELOCIDADE ENTRE PONTO {startLocationCount} VS PONTO {endLocationCount}: {transportationSpeedInKmsPerHour} km/h \n");

        var transportationVehicle = transportationSpeedInKmsPerHour.GetTransportationMethod();

        Console.WriteLine($"VEICULO UTILIZADO ENTRE PONTO {startLocationCount} VS PONTO {endLocationCount}: {transportationVehicle} \n");

        Console.WriteLine("---------------------------------------------------------------------------- \n");

        Task.Delay(6000);

        locationPoint++;
    }

    Console.WriteLine($"Your data was analyzed, based on {locations.Count()} lines");
    Console.WriteLine($"DISTANCIA PERCORIDA NO TOTAL {Math.Round(totalTraveledDistance, 2)} METROS");
    Console.WriteLine($"TEMPO GASTO NO TOTAL {Math.Round(totalSpentTime, 2)} SEGUNDOS");


    csvReader.Dispose();
    streamReader.Dispose();
}
catch (Exception ex)
{
    HandleException(ex);
}

void HandleException(Exception ex)
{
    if (ex is FileNotFoundException)
    {
        Console.WriteLine("File was not found, try again!");
        Console.WriteLine(ex.Message);

        return;
    }

    if (ex is HeaderValidationException)
    {
        Console.WriteLine("CSV is not in right format");
        Console.WriteLine(ex.Message);

        return;
    }

    Console.WriteLine("Unexpected error happened, try again later");
    Console.WriteLine(ex.Message);
}

IEnumerable<Location> FilterCsvRecords(CsvReader csvReader, int totalLinesToSkip = 0)
{
    var locations = new List<Location>();

    while (csvReader.Read())
    {
        try
        {
            csvReader.Context.RegisterClassMap<LocationMap>();

            var location = csvReader.GetRecord<Location>();

            if (location is not null && IsRecordValid(location))
                locations.Add(location);
        }
        catch (CsvHelper.TypeConversion.TypeConverterException ex)
        {
            Console.WriteLine($"Error processing record: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading CSV file: {ex.Message}");
        }
    }

    return locations.Skip(totalLinesToSkip);

    bool IsRecordValid(Location location)
    {
        if (!double.TryParse(location.Latitude.ToString(), out var latitude) ||
            latitude <= -90 || latitude >= 90)
            return false;

        if (!double.TryParse(location.Longitude.ToString(), out var longitude) ||
            longitude <= -180 || longitude >= 180)
            return false;

        if (!int.TryParse(location.ZeroValue.ToString(), out var zeroValue) ||
            (zeroValue != 0 && zeroValue != 1))
            return false;

        if (!double.TryParse(location.Altitude.ToString(), out var altitude) ||
            altitude <= -1000 || altitude >= 10000)
            return false;

        if (!DateTime.TryParse($"{location.Date} {location.Time}", out var date))
            return false;

        return true;
    }
}

public class LocationMap : ClassMap<Location>
{
    public LocationMap()
    {
        Map(m => m.Latitude).Name("Latitude");
        Map(m => m.Longitude).Name("Longitude");
        Map(m => m.Altitude).Name("Altitude");
        Map(m => m.ZeroValue).Name("ZeroValue");
        Map(m => m.Time).Name("Time");
        Map(m => m.NumberOfDays).Name("NumberOfDays");
        Map(m => m.Date).Name("Date");
    }

  
}