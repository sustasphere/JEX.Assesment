namespace JEX.Assessment.Persistence.V1.Types.Sets;

public class CompanyAddressSet
{
    public int Id { get; set; }
    public string FullAddress { get; set; } = null!;
    public int CompanyId { get; set; }
    public CompanySet CompanySet { get; set; } = null!;
}