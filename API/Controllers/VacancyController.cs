using JEX.Assessment.Application.V1.Types.Responses;
using Microsoft.AspNetCore.Mvc;

namespace JEX.Assessment.API.Controllers;

[ApiController]
[Route( "api/[controller]" )]
public class VacancyController : ControllerBase
{

    [HttpGet( "Get/{count?}", Name = "GetVacancies" )]
    [EndpointSummary( "Returns a list of vacancie" )]
    [EndpointDescription( "Simple fixed vacancy list" )]
    [ProducesResponseType<VacancyResponse>( StatusCodes.Status200OK, "application/json" )]
    public IActionResult Get( int count )
        => Ok(
            VacancyResponse.Generate( Guid.NewGuid(), count ) );
}