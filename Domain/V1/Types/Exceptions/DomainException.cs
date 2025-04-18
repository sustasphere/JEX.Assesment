namespace JEX.Assessment.Domain.V1.Types.Exceptions;

[Serializable]
public class DomainException : Exception
{
    public Guid CorrelationId { get; init; }

    public DomainException( ) : base() { }
    public DomainException( string message ) : base( message ) { }
    public DomainException( string message, Exception inner ) : base( message, inner ) { }
    public DomainException( string message, Exception inner, Guid correlationId ) : base( message, inner )
    {
        CorrelationId = correlationId;
    }
}
