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

    Console.WriteLine($"PONTO {locationPoint} VS PONTO {locationPoint + 1}");

    var initialTime = DateTime.Parse($"{startLocation.Date.ToString("MM/dd/yyyy")} {startLocation.Time}");
    var endTime = DateTime.Parse($"{endLocation.Date.ToString("MM/dd/yyyy")} {endLocation.Time}");

    Console.WriteLine($"TEMPO DECORRIDO: {initialTime.CalculateTimeDifferenceInSeconds(endTime)}");

    locationPoint++;
}

csvReader.Dispose();
streamReader.Dispose();