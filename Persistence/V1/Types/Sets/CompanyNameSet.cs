namespace JEX.Assessment.Persistence.V1.Types.Sets;

public class CompanyNameSet
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public int CompanyId { get; set; }
    public CompanySet CompanySet { get; set; } = null!;
}
