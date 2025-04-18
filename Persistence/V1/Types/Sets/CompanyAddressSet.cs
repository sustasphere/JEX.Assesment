using static System.String;

namespace JEX.Assessment.Persistence.V1.Types.Sets;

public class CompanyAddressSet
{
    public int Id { get; set; }
    public string FullAddress { get; set; } = Empty;
}