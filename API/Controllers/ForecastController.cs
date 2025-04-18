using JEX.Assessment.Application.V1.Types.Responses;
using Microsoft.AspNetCore.Mvc;

namespace JEX.Assessment.API.Controllers;

[ApiController]
[Route( "api/[controller]" )]
public class ForecastController : ControllerBase
{

    [HttpGet( "Generate/{days?}", Name = "GenerateWeatherForecast" )]
    [EndpointSummary( "Returns a weather forecast" )]
    [EndpointDescription( "Simple fixed weather forecast" )]
    [ProducesResponseType<GenerateForecastResponse>( StatusCodes.Status200OK, "application/json" )]
    public IActionResult Generate( int days )
        => Ok(
            GenerateForecastResponse.Generate( Guid.NewGuid(), days ) );
}
