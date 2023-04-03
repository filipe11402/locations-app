using CsvHelper;
using System.Globalization;

var fileLocation = $@"{Directory.GetCurrentDirectory()}\..\..\..\..\..\assets\20081026094426.csv.csv";

using var streamReader = new StreamReader(Path.GetFullPath(fileLocation));

using var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);


csvReader.Dispose();
streamReader.Dispose();