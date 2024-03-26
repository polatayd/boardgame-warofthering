using BoardGame.WarOfTheRing.Fellowships.Application;
using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships;
using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Hunts;
using Microsoft.EntityFrameworkCore;

namespace BoardGame.WarOfTheRing.Fellowships.Infrastructure.Persistence.EntityFrameworkCore;

public class FellowshipDbContext : DbContext, IUnitOfWork
{
    public DbSet<Fellowship> Fellowships { get; set; }
    public DbSet<Hunting> Huntings { get; set; }
    
    public FellowshipDbContext(DbContextOptions<FellowshipDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FellowshipDbContext).Assembly);
    
    async Task<int> IUnitOfWork.SaveChangesAsync()
    {
        return await SaveChangesAsync().ConfigureAwait(false);
    }
}