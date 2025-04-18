using JEX.Assessment.Persistence.V1.Types.Sets;
using Microsoft.EntityFrameworkCore;

namespace JEX.Assessment.API.Context;
public class ApiContext : DbContext
{
    public ApiContext( DbContextOptions<ApiContext> options ) : base( options ) { }

    protected override void OnModelCreating( ModelBuilder builder )
    {
        builder.Entity<CompanySet>().HasMany( e => e.Names ).WithMany();
        builder.Entity<CompanySet>().HasMany( e => e.Addresses ).WithMany();
    }

    public DbSet<CompanySet> CompanySet { get; set; }
    public DbSet<VacancySet> VacancySet { get; set; }
}
