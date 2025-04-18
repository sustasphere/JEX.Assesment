namespace JEX.Assessment.Domain.V1.Types.Messages;
public record GenerateCompany
{
    public static string QueueName = "generate-company";

    public Guid CorrelationId { get; init; }
    public int Count { get; init; }

    public static GenerateCompany Create( Guid correlationId, int count )
        => new() { CorrelationId = correlationId, Count = count };
}
