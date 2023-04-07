using Locations.API.Requests;
using Locations.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Net;
using System.Net.Http.Headers;

namespace Locations.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LocationsController : ControllerBase
{
    private readonly ILocationService _locationService;

    public LocationsController(
        ILocationService locationService)
    {
        _locationService = locationService;
    }

    [HttpPost("save")]
    public async Task<IActionResult> Save([FromBody] SaveLocationRequest request) 
    {
        var isSaved = await _locationService
            .SaveLocationAsync(request);

        return isSaved ? 
            Ok() :
            BadRequest();
    }

    [HttpGet("file/{deviceId}/download")]
    [Produces("application/json", "text/csv")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FileContentResult))]
    public async Task<IActionResult> DownloadFile(
        [FromRoute] string deviceId)
    {
        var fileStream = await _locationService
            .DownloadLocationFileAsync(deviceId);

        if (fileStream is null) { return NotFound(); }

        Response.Headers.ContentDisposition = $"attachment;filename={deviceId}.csv";

        return File(fileStream.ToArray(), "text/csv");
    }
}
