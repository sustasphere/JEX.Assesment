using JEX.Assessment.Persistence.V1.Types.Sets;
using Microsoft.EntityFrameworkCore;

namespace JEX.Assessment.API.Context;
public class ApiContext : DbContext
{
    public ApiContext( DbContextOptions<ApiContext> options ) : base( options ) { }

    public DbSet<CompanySet> CompanySet { get; set; }
    public DbSet<VacancySet> VacancySet { get; set; }
}
