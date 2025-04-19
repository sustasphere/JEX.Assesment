using JEX.Assessment.Application.V1.Types.Requests;
using JEX.Assessment.Domain.V1.Types.Entities;
using JEX.Assessment.Persistence.V1.Types.Sets;
using static System.String;

namespace JEX.Assessment.API.Extensions;

public static class VacancyExtension
{
    public static VacancySet AsVacancySet( this SaveVacancyRequest request )
        => new VacancySet() {
            VacancyGuid = request.VacancyId.ToString(),
            CompanyGuid = request.CompanyId.ToString(),
            Title = request.Title is object ? request.Title : Empty,
            Description = request.Description is object ? request.Description : Empty,
            IsActive = request.IsActive,
        };

    public static List<VacancyEntity> AsVacancies( this IQueryable<VacancySet> vacancySets )
    {
        List<VacancyEntity> vacancies = [];
        foreach ( var vacancySet in vacancySets )
        {
            vacancies.Add( vacancySet.AsVacancy() );
        }
        return vacancies;
    }

    public static VacancyEntity AsVacancy( this VacancySet vacancySet )
        => new() {
            VacancyId = Guid.Parse( vacancySet.VacancyGuid ?? Guid.Empty.ToString() ),
            CompanyId = Guid.Parse( vacancySet.CompanyGuid ?? Guid.Empty.ToString() ),
            Title = vacancySet.Title ?? Empty,
            Description = vacancySet.Description ?? Empty,
            IsActive = vacancySet.IsActive,
        };
}
