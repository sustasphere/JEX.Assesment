using JEX.Assessment.API.Context;
using JEX.Assessment.API.Extensions;
using JEX.Assessment.Application.V1.Types.Requests;
using JEX.Assessment.Application.V1.Types.Responses;
using JEX.Assessment.Domain.V1.Types.Messages;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace JEX.Assessment.API.Controllers;

[ApiController]
[Route( "api/[controller]" )]
public class VacancyController( IRequestClient<GenerateVacancy> client, ApiContext dbCtx ) : ControllerBase
{

    [HttpGet( "Generate/{count?}", Name = "GenerateVacancies" )]
    [EndpointSummary( "Returns a list of vacancies" )]
    [EndpointDescription( "Simple fixed vacancy list" )]
    [ProducesResponseType<GenerateVacancyResponse>( StatusCodes.Status200OK, "application/json" )]
    [ProducesResponseType<ProblemDetails>( StatusCodes.Status400BadRequest, "application/problem+json" )]
    public async Task<IActionResult> GenerateAsync( int count )
    {
        var message = GenerateVacancy.Create( Guid.NewGuid(), count );
        var response = await client.GetResponse<GeneratedVacancies>( message );

        return Ok(
            GenerateVacancyResponse.From( response ) );
    }

    [HttpGet( "GetAll", Name = "GetVacancies" )]
    [EndpointSummary( "Returns a list of vacancies" )]
    [EndpointDescription( "Simple fixed vacancy list" )]
    [ProducesResponseType<GetVacancyResponse>( StatusCodes.Status200OK, "application/json" )]
    [ProducesResponseType<ProblemDetails>( StatusCodes.Status400BadRequest, "application/problem+json" )]
    public IActionResult GetAll( )
    {
        return Ok( new GetVacancyResponse() {
            CorrelationID = Guid.NewGuid(),
            Vacancies = dbCtx.Vacancies.AsVacancies()
        } );
    }

    [HttpPost( "Save", Name = "SaveVacancy" )]
    [EndpointSummary( "Saves a vacancy" )]
    [EndpointDescription( "Simple db-save of a given vacancy" )]
    [ProducesResponseType<GenerateForecastResponse>( StatusCodes.Status201Created, "application/json" )]
    [ProducesResponseType<ProblemDetails>( StatusCodes.Status400BadRequest, "application/problem+json" )]
    public async Task<IActionResult> SaveAsync( [FromBody] SaveVacancyRequest request )
    {
        dbCtx.Add( request.AsVacancySet() );
        _ = await dbCtx.SaveChangesAsync();
        return Created();
    }
}