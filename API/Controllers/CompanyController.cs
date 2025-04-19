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
public class CompanyController( IRequestClient<GenerateCompany> client, ApiContext dbCtx ) : ControllerBase
{

    [HttpGet( "Generate/{count?}", Name = "GenerateCompanies" )]
    [EndpointSummary( "Returns a list of companies" )]
    [EndpointDescription( "Simple fixed company list" )]
    [ProducesResponseType<GenerateForecastResponse>( StatusCodes.Status200OK, "application/json" )]
    [ProducesResponseType<ProblemDetails>( StatusCodes.Status400BadRequest, "application/problem+json" )]
    public async Task<IActionResult> GenerateAsync( int count )
    {
        var message = GenerateCompany.Create( Guid.NewGuid(), count );
        var response = await client.GetResponse<GeneratedCompanies>( message );

        return Ok(
            GenerateCompanyResponse.From( response ) );
    }

    [HttpGet( "GetAll", Name = "GetCompanies" )]
    [EndpointSummary( "Returns a list of companies" )]
    [EndpointDescription( "Simple db company list" )]
    [ProducesResponseType<GetVacancyResponse>( StatusCodes.Status200OK, "application/json" )]
    [ProducesResponseType<ProblemDetails>( StatusCodes.Status400BadRequest, "application/problem+json" )]
    public IActionResult GetAll( ) => Ok( new GetCompanyResponse() {
        CorrelationID = Guid.NewGuid(),
        Companies = dbCtx.Companies.AsCompanies()
    } );

    [HttpGet( "GetAllActive", Name = "GetActiveCompanies" )]
    [EndpointSummary( "Returns a list of companies with at least one active vacancy" )]
    [EndpointDescription( "Simple db company list" )]
    [ProducesResponseType<GetVacancyResponse>( StatusCodes.Status200OK, "application/json" )]
    [ProducesResponseType<ProblemDetails>( StatusCodes.Status400BadRequest, "application/problem+json" )]
    public IActionResult GetAllActive( )
        => Ok( new GetCompanyResponse() {
            CorrelationID = Guid.NewGuid(),
            Companies = dbCtx.Companies.AsActiveCompanies( dbCtx.Vacancies )
        } );

    [HttpPost( "Save", Name = "SaveCompany" )]
    [EndpointSummary( "Saves a company" )]
    [EndpointDescription( "Simple db-save of a given company" )]
    [ProducesResponseType<GenerateForecastResponse>( StatusCodes.Status201Created, "application/json" )]
    [ProducesResponseType<ProblemDetails>( StatusCodes.Status400BadRequest, "application/problem+json" )]
    public async Task<IActionResult> SaveAsync( [FromBody] SaveCompanyRequest request )
    {
        dbCtx.Add( request.AsCompanySet() );
        _ = await dbCtx.SaveChangesAsync();
        return Created();
    }
}
