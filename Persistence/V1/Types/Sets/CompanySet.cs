namespace JEX.Assessment.Persistence.V1.Types.Sets;
public class CompanySet
{
    public int Id { get; set; }
    public required string CompanyGuid { get; set; }
    public required string Names { get; set; }
    public required string Addresses { get; set; }
}
