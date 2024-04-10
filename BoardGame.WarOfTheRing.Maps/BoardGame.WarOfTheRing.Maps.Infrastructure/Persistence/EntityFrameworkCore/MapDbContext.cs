using BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps;
using Microsoft.EntityFrameworkCore;

namespace BoardGame.WarOfTheRing.Maps.Infrastructure.Persistence.EntityFrameworkCore;

public class MapDbContext : DbContext
{
    public DbSet<Map> Maps { get; set; }

    public MapDbContext(DbContextOptions<MapDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MapDbContext).Assembly);
}