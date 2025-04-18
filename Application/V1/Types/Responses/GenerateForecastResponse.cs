using JEX.Assessment.Domain.V1.Types.Entities;

namespace JEX.Assessment.Application.V1.Types.Responses;

public record GenerateForecastResponse
{
    public Guid CorrelationID { get; init; }
    public required List<ForecastEntity> Forecasts { get; init; }

    public static GenerateForecastResponse Generate( Guid correlationId, int days )
        => new() {
            CorrelationID = correlationId,
            Forecasts = [ .. Enumerable.Range( 1, days ).Select( ForecastEntity.Generate ) ],
        };
}
