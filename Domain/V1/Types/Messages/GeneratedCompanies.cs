using JEX.Assessment.Domain.V1.Types.Entities;

namespace JEX.Assessment.Domain.V1.Types.Messages;

public record GeneratedCompanies
{
    public Guid CorrelationId { get; init; }
    public required List<CompanyEntity> Companies { get; init; }

    public static GeneratedCompanies Create( Guid correlationId, List<CompanyEntity> companies )
        => new() { CorrelationId = correlationId, Companies = companies };
}