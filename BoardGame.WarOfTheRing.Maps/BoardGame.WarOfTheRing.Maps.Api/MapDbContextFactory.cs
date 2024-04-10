using BoardGame.WarOfTheRing.Maps.Infrastructure.Persistence.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BoardGame.WarOfTheRing.Maps.Api;

public class MapDbContextFactory : IDesignTimeDbContextFactory<MapDbContext>
{
    public MapDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<MapDbContext>();
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("postgresql"));

        return new MapDbContext(optionsBuilder.Options);
    }
}