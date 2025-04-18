namespace JEX.Assessment.Domain.V1.Types.Messages;
public record FailedRequest
{
    public Guid CorrelationId { get; init; }
    public required string Reason { get; init; }
    public required string Trace { get; init; }

    public static FailedRequest Create( Guid correlationId, string reason, string trace = "" )
        => new() {
            CorrelationId = correlationId,
            Reason = reason,
            Trace = trace
        };
}
