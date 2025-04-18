using Bogus;

namespace JEX.Assessment.Domain.V1.Types.Entities;

public class CompanyAddress
{
    public required string FullAddress { get; init; }

    public static CompanyAddress Generate( Faker fake )
        => new() {
            FullAddress = fake.Address.FullAddress()
        };
}