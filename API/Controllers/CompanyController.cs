using JEX.Assessment.Application.V1.Types.Responses;
using Microsoft.AspNetCore.Mvc;

namespace JEX.Assessment.API.Controllers;

[ApiController]
[Route( "api/[controller]" )]
public class CompanyController : ControllerBase
{

    [HttpGet( "Get/{count?}", Name = "GetCompanies" )]
    [EndpointSummary( "Returns a list of companies" )]
    [EndpointDescription( "Simple fixed company list" )]
    [ProducesResponseType<ForecastResponse>( StatusCodes.Status200OK, "application/json" )]
    public IActionResult Get( int count )
        => Ok(
            CompanyResponse.Generate( Guid.NewGuid(), count ) );

}
