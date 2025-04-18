using Bogus;
using JEX.Assessment.Domain.V1.Types.Entities;
using JEX.Assessment.Domain.V1.Types.Messages;
using MassTransit;

namespace JEX.Assessment.Application.V1.Types.Responses;

public record GenerateVacancyResponse
{
    static readonly Faker _faker = new();

    public Guid CorrelationID { get; init; }
    public required List<VacancyEntity> Vacancies { get; init; }

    public static GenerateVacancyResponse Generate( Guid correlationId, int count )
        => new() {
            CorrelationID = correlationId,
            Vacancies = [ .. Enumerable.Range( 1, count ).Select( _ => VacancyEntity.Generate( _faker ) ) ]
        };

    public static GenerateVacancyResponse From( Response<GeneratedVacancies> response )
        => new() { CorrelationID = response.Message.CorrelationId, Vacancies = response.Message.Vacancies };
}
