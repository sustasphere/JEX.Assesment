using Bogus;
using JEX.Assessment.Domain.V1.Types.Entities;

namespace JEX.Assessment.Application.V1.Types.Responses;

public record CompanyResponse
{
    static readonly Faker _faker = new();

    public required Guid CorrelationID { get; init; }
    public required List<Company> Companies { get; init; }

    public static CompanyResponse Generate( Guid correlationId, int count )
        => new() {
            CorrelationID = correlationId,
            Companies = [ .. Enumerable.Range( 1, count ).Select( _ => Company.Generate( _faker ) ) ]
        };
}