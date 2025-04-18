using JEX.Assessment.Domain.V1.Types.Entities;

namespace JEX.Assessment.Application.V1.Types.Responses;

public record GetCompanyResponse
{
    public Guid CorrelationID { get; init; }
    public required List<CompanyEntity> Companies { get; init; }
}