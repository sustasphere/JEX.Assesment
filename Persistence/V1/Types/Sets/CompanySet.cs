namespace JEX.Assessment.Persistence.V1.Types.Sets;
public class CompanySet
{
    public int Id { get; set; }
    public string CompanyGuid { get; set; } = null!;
    public List<CompanyNameSet> Names { get; init; } = [];
    public List<CompanyAddressSet> Addresses { get; init; } = [];
}
