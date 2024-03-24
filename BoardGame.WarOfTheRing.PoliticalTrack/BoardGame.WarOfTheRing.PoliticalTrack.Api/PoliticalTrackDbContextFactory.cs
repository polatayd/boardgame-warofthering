using BoardGame.WarOfTheRing.PoliticalTrack.Infrastructure.Persistence.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BoardGame.WarOfTheRing.PoliticalTrack.Api;

public class PoliticalTrackDbContextFactory : IDesignTimeDbContextFactory<PoliticalTrackDbContext>
{
    public PoliticalTrackDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<PoliticalTrackDbContext>();
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("postgresql"));

        return new PoliticalTrackDbContext(optionsBuilder.Options);
    }
}