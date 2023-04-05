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

for (int totalLocation = 0; totalLocation < locations.Count; totalLocation++)
{
    if (totalLocation + 1 == locations.Count) { break; }

    var startLocation = locations[totalLocation];
    var endLocation = locations[totalLocation + 1];

    var startLocationCount = 1;
    var endLocationCount = startLocationCount++;

    Console.WriteLine($"PONTO {startLocationCount} VS PONTO {endLocationCount}");

    var initialTime = DateTime.Parse($"{startLocation.Date.ToString("MM/dd/yyyy")} {startLocation.Time}");
    var endTime = DateTime.Parse($"{endLocation.Date.ToString("MM/dd/yyyy")} {endLocation.Time}");
    var timeDifferece = initialTime.CalculateTimeDifferenceInSeconds(endTime);

    Console.WriteLine($"TEMPO DECORRIDO: {timeDifferece}s");

    var traveledDistance = LocationHelpers.CalculateTraveledDistanceInSeconds(startLocation, endLocation);

    Console.WriteLine($"DISTANCIA ENTRE PONTO {startLocationCount} VS PONTO {endLocationCount}: {traveledDistance}m");

    var transportationSpeed = traveledDistance.CalculateSpeedInMetersPerSecond(timeDifferece);

    Console.WriteLine($"VELOCIDADE ENTRE PONTO {startLocationCount} VS PONTO {endLocationCount}: {transportationSpeed} m/s");

    Task.Delay(2000);

    locationPoint++;
}

csvReader.Dispose();
streamReader.Dispose();