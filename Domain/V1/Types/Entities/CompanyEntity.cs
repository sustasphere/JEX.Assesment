using Bogus;

namespace JEX.Assessment.Domain.V1.Types.Entities;

public class CompanyEntity
{
    public Guid CompanyId { get; init; }
    public required List<VacancyEntity> Vacancies { get; init; }
    public required List<CompanyName> Names { get; init; }
    public required List<CompanyAddress> Addresses { get; init; }
    public bool IsActive { get; init; }

    public static CompanyEntity Generate( Faker faker )
        => new() {
            CompanyId = Guid.NewGuid(),
            Vacancies = [],
            Names = [ CompanyName.Generate( faker ) ],
            Addresses = [ CompanyAddress.Generate( faker ) ]
        };

    public static async Task<CompanyEntity> GenerateAsync( Faker faker, CancellationToken ct )
    {
        var producer = new TaskCompletionSource<CompanyEntity>( ct );

        producer.SetResult( Generate( faker ) );

        return await producer.Task;
    }
}
