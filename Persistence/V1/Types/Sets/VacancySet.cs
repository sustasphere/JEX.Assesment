namespace JEX.Assessment.Persistence.V1.Types.Sets;

public class VacancySet
{
    public int Id { get; set; }
    public required string CompanyGuid { get; set; }
    public required string VacancyGuid { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
    public bool IsActive { get; set; }
}