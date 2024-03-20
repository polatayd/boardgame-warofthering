using BoardGame.WarOfTheRing.PoliticalTrack.Application;
using BoardGame.WarOfTheRing.PoliticalTrack.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace BoardGame.WarOfTheRing.PoliticalTrack.Infrastructure.Persistence.EntityFrameworkCore;

public class PoliticalTrackDbContext : DbContext, IUnitOfWork
{
    public DbSet<Nation> Nations { get; set; }
    
    public PoliticalTrackDbContext(DbContextOptions<PoliticalTrackDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder?.ApplyConfigurationsFromAssembly(typeof(PoliticalTrackDbContext).Assembly);

    async Task<int> IUnitOfWork.SaveChangesAsync()
    {
        return await SaveChangesAsync().ConfigureAwait(false);
    }
}