using BoardGame.WarOfTheRing.Maps.Application;
using BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps;
using Microsoft.EntityFrameworkCore;

namespace BoardGame.WarOfTheRing.Maps.Infrastructure.Persistence.EntityFrameworkCore;

public class MapDbContext : DbContext, IUnitOfWork
{
    public DbSet<Map> Maps { get; set; }

    public MapDbContext(DbContextOptions<MapDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MapDbContext).Assembly);
    
    async Task<int> IUnitOfWork.SaveChangesAsync()
    {
        var result = await SaveChangesAsync().ConfigureAwait(false);

        return result;
    }
}