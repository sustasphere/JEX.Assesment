using JEX.Assessment.Domain.V1.Types.Entities;

namespace JEX.Assessment.Application.V1.Types.Responses;
public record GetVacancyResponse
{
    public Guid CorrelationID { get; init; }
    public required List<VacancyEntity> Vacancies { get; init; }
}
