using CsvHelper;
using CsvHelper.Configuration;
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

foreach (var location in csvReader.GetRecords<Location>())
{
    ;
}

csvReader.Dispose();
streamReader.Dispose();