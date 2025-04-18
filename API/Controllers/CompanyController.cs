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
        var dbSet = dbCtx.CompanySet;
        List<CompanyEntity> companies = [];
        foreach ( var set in dbSet )
        {
            var fullName = set.Names.Any() ? set.Names.First().FullName : string.Empty;
            var fullAddress = set.Addresses.Any() ? set.Addresses.First().FullAddress : string.Empty;
            companies.Add( new CompanyEntity() {
                CompanyId = Guid.Parse( set.CompanyGuid ?? Guid.Empty.ToString() ),
                // ToDo: implement proper company-name mapping
                Names = [ new() { FullName = fullName } ],
                // ToDo: implement proper company-address mapping
                Addresses = [ new() { FullAddress = fullAddress } ]
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
        var fullName = request.Names.Any() ? request.Names.First().FullName : string.Empty;
        var fullAddress = request.Addresses.Any() ? request.Addresses.First().FullAddress : string.Empty;
        var set = new CompanySet() {
            CompanyGuid = request.CompanyId.ToString(),
            // ToDo: implement proper company-name mapping
            Names = new List<CompanyNameSet> { new() { FullName = fullName } },
            // ToDo: implement proper company-address mapping
            Addresses = new List<CompanyAddressSet> { new() { FullAddress = fullAddress } }
        };
        dbCtx.Add( set );
        _ = await dbCtx.SaveChangesAsync();
        return Created();
    }
}
