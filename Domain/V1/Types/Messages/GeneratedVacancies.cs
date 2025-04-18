using JEX.Assessment.Domain.V1.Types.Entities;

namespace JEX.Assessment.Domain.V1.Types.Messages;

public record GeneratedVacancies
{
    public Guid CorrelationId { get; init; }
    public required List<VacancyEntity> Vacancies { get; init; }

    public static GeneratedVacancies Create( Guid correlationId, List<VacancyEntity> vacancies )
        => new() { CorrelationId = correlationId, Vacancies = vacancies };
}
