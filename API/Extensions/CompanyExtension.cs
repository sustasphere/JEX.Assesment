using JEX.Assessment.Application.V1.Types.Requests;
using JEX.Assessment.Domain.V1.Types.Entities;
using JEX.Assessment.Persistence.V1.Types.Sets;
using static System.String;

namespace JEX.Assessment.API.Extensions;

public static class CompanyExtension
{
    public static CompanySet AsCompanySet( this SaveCompanyRequest request )
    {
        var names = request.Names.Any()
            ? Join( ";", request.Names.Select( n => n.FullName ) )
            : Empty;
        var addresses = request.Addresses.Any()
            ? Join( ";", request.Addresses.Select( a => a.FullAddress ) )
            : Empty;

        return new CompanySet() {
            CompanyGuid = request.CompanyId.ToString(),
            Names = names,
            Addresses = addresses
        };
    }

    public static List<CompanyEntity> AsActiveCompanies(
        this IQueryable<CompanySet> companySets,
        IQueryable<VacancySet> withVacancySets )
    {
        List<CompanyEntity> companies = [];
        var activeVacancies = withVacancySets.Where( v => v.IsActive );
        foreach ( var companySet in companySets.Where( c => c.IsActive ) )
        {
            var withActiveVacancySets = activeVacancies
                .Where( v => v.CompanyGuid == companySet.CompanyGuid );

            companies.Add( companySet.AsActiveCompany( withActiveVacancySets.AsVacancies() ) );
        }

        return companies;
    }

    public static List<CompanyEntity> AsCompanies( this IQueryable<CompanySet> companySets )
    {
        List<CompanyEntity> companies = [];
        foreach ( var companySet in companySets )
        {
            companies.Add( companySet.AsCompany() );
        }

        return companies;
    }

    public static CompanyEntity AsActiveCompany( this CompanySet companySet, List<VacancyEntity> withVacancies )
        => companySet.Create( withVacancies );

    public static CompanyEntity AsCompany( this CompanySet companySet )
        => companySet.Create();

    static CompanyEntity Create( this CompanySet companySet, List<VacancyEntity>? vacancies = default )
        => new() {
            CompanyId = Guid.Parse( companySet.CompanyGuid ?? Guid.Empty.ToString() ),
            Vacancies = vacancies is object ? vacancies : new List<VacancyEntity>(),
            Names = companySet.AsNames().ToList(),
            Addresses = companySet.AsAddresses().ToList(),
        };

    public static IEnumerable<CompanyName> AsNames( this CompanySet companySet )
        => IsNullOrEmpty( companySet.Names )
                ? [ new CompanyName { FullName = Empty } ]
                : companySet.Names.Split( ";", StringSplitOptions.RemoveEmptyEntries )
                    .Select( name => new CompanyName { FullName = name } );

    public static IEnumerable<CompanyAddress> AsAddresses( this CompanySet companySet )
        => IsNullOrEmpty( companySet.Addresses )
                ? [ new CompanyAddress { FullAddress = Empty } ]
                : companySet.Addresses.Split( ";", StringSplitOptions.RemoveEmptyEntries )
                    .Select( address => new CompanyAddress { FullAddress = address } );
}
