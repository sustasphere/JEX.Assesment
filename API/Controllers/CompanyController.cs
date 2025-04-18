using JEX.Assessment.API.Context;
using JEX.Assessment.Application.V1.Types.Requests;
using JEX.Assessment.Application.V1.Types.Responses;
using JEX.Assessment.Domain.V1.Types.Entities;
using JEX.Assessment.Domain.V1.Types.Messages;
using JEX.Assessment.Persistence.V1.Types.Sets;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using static System.String;

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
    [EndpointDescription( "Simple fixed company list" )]
    [ProducesResponseType<GetVacancyResponse>( StatusCodes.Status200OK, "application/json" )]
    [ProducesResponseType<ProblemDetails>( StatusCodes.Status400BadRequest, "application/problem+json" )]
    public IActionResult GetAll( )
    {
        List<CompanyEntity> companies = [];
        foreach ( var set in dbCtx.Companies )
        {
            var names = IsNullOrEmpty( set.Names )
                ? [ new CompanyName { FullName = string.Empty } ]
                : set.Names.Split( ";", StringSplitOptions.RemoveEmptyEntries ).Select( name => new CompanyName { FullName = name } );
            var addresses = IsNullOrEmpty( set.Addresses )
                ? [ new CompanyAddress { FullAddress = string.Empty } ]
                : set.Names.Split( ";", StringSplitOptions.RemoveEmptyEntries ).Select( name => new CompanyAddress { FullAddress = name } );

            companies.Add( new CompanyEntity() {
                CompanyId = Guid.Parse( set.CompanyGuid ?? Guid.Empty.ToString() ),
                Names = names.ToList(),
                Addresses = addresses.ToList()
            } );
        }

        return Ok( new GetCompanyResponse() { CorrelationID = Guid.NewGuid(), Companies = companies } );
    }

    [HttpPost( "Save", Name = "SaveCompany" )]
    [EndpointSummary( "Saves a company" )]
    [EndpointDescription( "Simple db-save of a given company" )]
    [ProducesResponseType<GenerateForecastResponse>( StatusCodes.Status201Created, "application/json" )]
    [ProducesResponseType<ProblemDetails>( StatusCodes.Status400BadRequest, "application/problem+json" )]
    public async Task<IActionResult> SaveAsync( [FromBody] SaveCompanyRequest request )
    {
        var names = request.Names.Any() ? Join( ";", request.Names.Select( n => n.FullName ) ) : string.Empty;
        var addresses = request.Addresses.Any() ? Join( ";", request.Addresses.Select( a => a.FullAddress ) ) : string.Empty;

        dbCtx.Add( new CompanySet() {
            CompanyGuid = request.CompanyId.ToString(),
            Names = names,
            Addresses = addresses
        } );
        _ = await dbCtx.SaveChangesAsync();
        return Created();
    }
}
