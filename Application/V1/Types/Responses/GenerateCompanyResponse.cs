using Bogus;
using JEX.Assessment.Domain.V1.Types.Entities;
using JEX.Assessment.Domain.V1.Types.Messages;
using MassTransit;

namespace JEX.Assessment.Application.V1.Types.Responses;

public record GenerateCompanyResponse
{
    static readonly Faker _faker = new();

    public Guid CorrelationID { get; init; }
    public required List<CompanyEntity> Companies { get; init; }

    public static GenerateCompanyResponse Generate( Guid correlationId, int count )
        => new() {
            CorrelationID = correlationId,
            Companies = [ .. Enumerable.Range( 1, count ).Select( _ => CompanyEntity.Generate( _faker ) ) ]
        };

    public static GenerateCompanyResponse From( Response<GeneratedCompanies> response )
        => new() { CorrelationID = response.Message.CorrelationId, Companies = response.Message.Companies };
}