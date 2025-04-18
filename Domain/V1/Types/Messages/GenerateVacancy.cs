namespace JEX.Assessment.Domain.V1.Types.Messages;

public record GenerateVacancy
{
    public static string QueueName = "generate-vacancy";

    public Guid CorrelationId { get; init; }
    public int Count { get; init; }

    public static GenerateVacancy Create( Guid correlationId, int count )
        => new() { CorrelationId = correlationId, Count = count };
}
