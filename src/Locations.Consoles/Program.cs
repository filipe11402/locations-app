using CsvHelper;
using CsvHelper.Configuration;
using Locations.API.Helpers;
using Locations.API.Models;
using System.Globalization;
using System.Text;

var fileLocation = $@"{Directory.GetCurrentDirectory()}\..\..\..\..\..\assets\20081026094426.csv.csv";

using var streamReader = new StreamReader(Path.GetFullPath(fileLocation));

var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
{
    Encoding = Encoding.UTF8,
    Delimiter = ","
};
using var csvReader = new CsvReader(streamReader, configuration);

int locationPoint = 1;

var locations = csvReader.GetRecords<Location>()
    .ToList();

locations = locations.Where(record => record.Latitude > -90 || record.Latitude < 90).ToList()
    .Where(record => record.Longitude > -180 || record.Longitude < 180).ToList()
    .Where(record => record.ZeroValue == 0 || record.ZeroValue == 1).ToList()
    .Where(record => record.Altitude > -1000 || record.Latitude < 10000).ToList()
    .Where(record => record.Latitude > -90 || record.Latitude < 90).ToList();

var totalTraveledDistance = 0.0;

var totalSpentTime = 0.0;

for (int totalLocation = 0; totalLocation < locations.Count; totalLocation++)
{
    if (totalLocation + 1 == locations.Count) { break; }

    var startLocation = locations[totalLocation];
    var endLocation = locations[totalLocation + 1];

    var startLocationCount = 1;
    var endLocationCount = startLocationCount++;

    Console.WriteLine($"PONTO {startLocationCount} VS PONTO {endLocationCount}");

    var initialTime = DateTime.Parse($"{startLocation.Date} {startLocation.Time}");
    var endTime = DateTime.Parse($"{endLocation.Date} {endLocation.Time}");
    var timeDifferece = initialTime.CalculateTimeDifferenceInSeconds(endTime);

    totalSpentTime += timeDifferece;

    Console.WriteLine($"TEMPO DECORRIDO: {timeDifferece}s");

    var traveledDistance = LocationHelpers.CalculateTraveledDistanceInSeconds(startLocation, endLocation);

    totalTraveledDistance += traveledDistance;

    Console.WriteLine($"DISTANCIA ENTRE PONTO {startLocationCount} VS PONTO {endLocationCount}: {traveledDistance}m");

    var transportationSpeed = traveledDistance.CalculateSpeedInMetersPerSecond(timeDifferece);

    Console.WriteLine($"VELOCIDADE ENTRE PONTO {startLocationCount} VS PONTO {endLocationCount}: {transportationSpeed} m/s");

    var transportationSpeedInKmsPerHour = transportationSpeed.CalculateSpeedInKmPerHour();

    Console.WriteLine($"VELOCIDADE ENTRE PONTO {startLocationCount} VS PONTO {endLocationCount}: {transportationSpeedInKmsPerHour} km/h");

    var transportationVehicle = transportationSpeedInKmsPerHour.GetTransportationMethod();

    Console.WriteLine($"VEICULO UTILIZADO ENTRE PONTO {startLocationCount} VS PONTO {endLocationCount}: {transportationVehicle} ");

    Task.Delay(2000);

    locationPoint++;
}

Console.WriteLine($"DISTANCIA PERCORIDA NO TOTAL {Math.Round(totalTraveledDistance, 2)} METROS");

Console.WriteLine($"TEMPO GASTO NO TOTAL {Math.Round(totalSpentTime, 2)} SEGUNDOS");

csvReader.Dispose();
streamReader.Dispose();