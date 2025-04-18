using JEX.Assessment.Application.V1.Types.Responses;
using JEX.Assessment.Domain.V1.Types.Messages;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace JEX.Assessment.API.Controllers;

[ApiController]
[Route( "api/[controller]" )]
public class VacancyController( IRequestClient<GenerateVacancy> client ) : ControllerBase
{

    [HttpGet( "Generate/{count?}", Name = "GenerateVacancies" )]
    [EndpointSummary( "Returns a list of vacancie" )]
    [EndpointDescription( "Simple fixed vacancy list" )]
    [ProducesResponseType<VacancyResponse>( StatusCodes.Status200OK, "application/json" )]
    [ProducesResponseType<ProblemDetails>( StatusCodes.Status400BadRequest, "application/problem+json" )]
    public async Task<IActionResult> GenerateAsync( int count )
    {
        var message = GenerateVacancy.Create( Guid.NewGuid(), count );
        var response = await client.GetResponse<GeneratedVacancies>( message );

        return Ok(
            VacancyResponse.From( response ) );
    }
}