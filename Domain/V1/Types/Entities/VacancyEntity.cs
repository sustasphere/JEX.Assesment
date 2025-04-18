using Bogus;

namespace JEX.Assessment.Domain.V1.Types.Entities;

public class VacancyEntity
{
    public Guid VacancyId { get; init; }
    public Guid CompanyId { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
    public bool IsActive { get; init; }

    public static VacancyEntity Generate( Faker faker )
        => new() {
            VacancyId = Guid.NewGuid(),
            CompanyId = Guid.NewGuid(),
            Title = faker.Lorem.Sentences( 1 ),
            Description = faker.Lorem.Sentences( 6 ),
            IsActive = true
        };

    public static async Task<VacancyEntity> GenerateAsync( Faker faker, CancellationToken ct )
    {
        var producer = new TaskCompletionSource<VacancyEntity>( ct );

        producer.SetResult( Generate( faker ) );

        return await producer.Task;
    }
}