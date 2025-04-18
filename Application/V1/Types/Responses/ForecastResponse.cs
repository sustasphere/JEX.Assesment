using JEX.Assessment.Domain.V1.Types.Entities;

namespace JEX.Assessment.Application.V1.Types.Responses;

public record ForecastResponse
{
    public required Guid CorrelationID { get; init; }
    public required List<Forecast> Forecasts { get; init; }

    public static ForecastResponse Generate(Guid correlationId, int days)
        => new() {
            CorrelationID = correlationId,
            Forecasts = Enumerable.Range(1, days).Select(Forecast.Generate).ToList(),
        };
}
