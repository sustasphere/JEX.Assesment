using JEX.Assessment.Application.V1.Types.Responses;
using JEX.Assessment.Domain.V1.Types.Messages;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace JEX.Assessment.API.Controllers;

[ApiController]
[Route( "api/[controller]" )]
public class CompanyController( IRequestClient<GenerateCompany> client ) : ControllerBase
{

    [HttpGet( "Generate/{count?}", Name = "GenerateCompanies" )]
    [EndpointSummary( "Returns a list of companies" )]
    [EndpointDescription( "Simple fixed company list" )]
    [ProducesResponseType<ForecastResponse>( StatusCodes.Status200OK, "application/json" )]
    [ProducesResponseType<ProblemDetails>( StatusCodes.Status400BadRequest, "application/problem+json" )]
    public async Task<IActionResult> GenerateAsync( int count )
    {
        var message = GenerateCompany.Create( Guid.NewGuid(), count );
        var response = await client.GetResponse<GeneratedCompanies>( message );

        return Ok(
            CompanyResponse.From( response ) );
    }
}
