namespace JEX.Assessment.Application.V1.Types.Requests;

public class SaveVacancyRequest
{
    public Guid VacancyId { get; set; }
    public Guid CompanyId { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
}