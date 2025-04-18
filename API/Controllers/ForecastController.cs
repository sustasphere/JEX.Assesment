using JEX.Assessment.Application.V1.Types.Responses;
using Microsoft.AspNetCore.Mvc;

namespace JEX.Assessment.API.Controllers;

[ApiController]
[Route( "api/[controller]" )]
public class ForecastController : ControllerBase
{

    [HttpGet( "Get/{days?}", Name = "GetWeatherForecast" )]
    [EndpointSummary( "Returns a weather forecast" )]
    [EndpointDescription( "Simple fixed weather forecast" )]
    [ProducesResponseType<ForecastResponse>( StatusCodes.Status200OK, "application/json" )]
    public IActionResult Get( int days )
        => Ok(
            ForecastResponse.Generate( Guid.NewGuid(), days ) );
}
