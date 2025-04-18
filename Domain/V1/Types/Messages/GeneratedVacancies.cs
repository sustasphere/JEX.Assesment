using JEX.Assessment.Domain.V1.Types.Entities;

namespace JEX.Assessment.Domain.V1.Types.Messages;

public record GeneratedVacancies
{
    public Guid CorrelationId { get; init; }
    public required List<Vacancy> Vacancies { get; init; }

    public static GeneratedVacancies Create( Guid correlationId, List<Vacancy> vacancies )
        => new() { CorrelationId = correlationId, Vacancies = vacancies };
}
