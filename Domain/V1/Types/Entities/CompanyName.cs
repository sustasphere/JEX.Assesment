using Bogus;

namespace JEX.Assessment.Domain.V1.Types.Entities;

public class CompanyName
{
    public required string FullName { get; init; }

    public static CompanyName Generate( Faker fake )
        => new() {
            FullName = fake.Company.CompanyName()
        };
}
