using Bogus;

namespace JEX.Assessment.Domain.V1.Types.Entities;

public class Vacancy
{
    public required Guid VacancyId { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }

    public static Vacancy Generate( Faker faker )
        => new() {
            VacancyId = Guid.NewGuid(),
            Title = faker.Lorem.Sentences( 1 ),
            Description = faker.Lorem.Sentences( 6 ),
        };

    public static async Task<Vacancy> GenerateAsync( Faker faker, CancellationToken ct )
    {
        var producer = new TaskCompletionSource<Vacancy>( ct );

        producer.SetResult( Generate( faker ) );

        return await producer.Task;
    }
}