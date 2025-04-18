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

    public static async Task<Company> GenerateAsync( Faker faker, CancellationToken ct )
    {
        var producer = new TaskCompletionSource<Company>( ct );

        producer.SetResult( Generate( faker ) );

        return await producer.Task;
    }
}
