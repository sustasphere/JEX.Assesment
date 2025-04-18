using Bogus;
using JEX.Assessment.Domain.V1.Types.Entities;
using JEX.Assessment.Domain.V1.Types.Messages;
using MassTransit;

namespace JEX.Assessment.Application.V1.Types.Responses;

public record VacancyResponse
{
    static readonly Faker _faker = new();

    public Guid CorrelationID { get; init; }
    public required List<Vacancy> Vacancies { get; init; }

    public static VacancyResponse Generate( Guid correlationId, int count )
        => new() {
            CorrelationID = correlationId,
            Vacancies = [ .. Enumerable.Range( 1, count ).Select( _ => Vacancy.Generate( _faker ) ) ]
        };

    public static VacancyResponse From( Response<GeneratedVacancies> response )
        => new() { CorrelationID = response.Message.CorrelationId, Vacancies = response.Message.Vacancies };
}
