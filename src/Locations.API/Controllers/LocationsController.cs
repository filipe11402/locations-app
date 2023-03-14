using Locations.API.Requests;
using Locations.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        var isSaved = await _locationService.SaveLocationAsync(request);

        return isSaved ? 
            Ok() :
            BadRequest();
    }
}
