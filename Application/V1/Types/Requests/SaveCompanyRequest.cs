using JEX.Assessment.Domain.V1.Types.Entities;

namespace JEX.Assessment.Application.V1.Types.Requests;
public class SaveCompanyRequest
{
    public Guid CompanyId { get; set; }
    public required List<CompanyName> Names { get; init; }
    public required List<CompanyAddress> Addresses { get; init; }
}
