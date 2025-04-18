using Bogus;

namespace JEX.Assessment.Domain.V1.Types.Entities;

public class Company
{
    public required Guid CompanyId { get; init; }
    public required List<CompanyName> Names { get; init; }
    public required List<CompanyAddress> Addresses { get; init; }

    public static Company Generate( Faker faker )
        => new() {
            CompanyId = Guid.NewGuid(),
            Names = [ CompanyName.Generate( faker ) ],
            Addresses = [ CompanyAddress.Generate( faker ) ]
        };
}
