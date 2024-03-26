using BoardGame.WarOfTheRing.Fellowships.Infrastructure.Persistence.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BoardGame.WarOfTheRing.Fellowships.Api;

public class FellowshipDbContextFactory : IDesignTimeDbContextFactory<FellowshipDbContext>
{
    public FellowshipDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<FellowshipDbContext>();
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("postgresql"));

        return new FellowshipDbContext(optionsBuilder.Options);
    }
}