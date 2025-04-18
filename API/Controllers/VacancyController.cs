using JEX.Assessment.API.Context;
using JEX.Assessment.Application.V1.Types.Requests;
using JEX.Assessment.Application.V1.Types.Responses;
using JEX.Assessment.Domain.V1.Types.Entities;
using JEX.Assessment.Domain.V1.Types.Messages;
using JEX.Assessment.Persistence.V1.Types.Sets;
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
        List<VacancyEntity> vacancies = [];
        foreach ( var set in dbCtx.Vacancies )
        {
            vacancies.Add( new VacancyEntity() {
                VacancyId = Guid.Parse( set.VacancyGuid ?? Guid.Empty.ToString() ),
                CompanyId = Guid.Parse( set.CompanyGuid ?? Guid.Empty.ToString() ),
                Title = set.Title ?? string.Empty,
                Description = set.Description ?? string.Empty
            } );
        }

        return Ok( new GetVacancyResponse() { CorrelationID = Guid.NewGuid(), Vacancies = vacancies } );
    }

    [HttpPost( "Save", Name = "SaveVacancy" )]
    [EndpointSummary( "Saves a vacancy" )]
    [EndpointDescription( "Simple db-save of a given vacancy" )]
    [ProducesResponseType<GenerateForecastResponse>( StatusCodes.Status201Created, "application/json" )]
    [ProducesResponseType<ProblemDetails>( StatusCodes.Status400BadRequest, "application/problem+json" )]
    public async Task<IActionResult> SaveAsync( [FromBody] SaveVacancyRequest request )
    {
        dbCtx.Add( new VacancySet() {
            VacancyGuid = request.VacancyId.ToString(),
            CompanyGuid = request.CompanyId.ToString(),
            Title = request.Title,
            Description = request.Description,
            IsActive = true
        } );
        _ = await dbCtx.SaveChangesAsync();
        return Created();
    }
}