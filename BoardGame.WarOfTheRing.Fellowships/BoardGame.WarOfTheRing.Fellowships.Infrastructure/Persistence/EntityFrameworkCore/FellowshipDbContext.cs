using BoardGame.WarOfTheRing.Fellowships.Application;
using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships;
using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Hunts;
using BoardGame.WarOfTheRing.Fellowships.Domain.Base;
using BoardGame.WarOfTheRing.Fellowships.Infrastructure.DomainEventDispatcher;
using Microsoft.EntityFrameworkCore;

namespace BoardGame.WarOfTheRing.Fellowships.Infrastructure.Persistence.EntityFrameworkCore;

public class FellowshipDbContext : DbContext, IUnitOfWork
{
    private readonly IDomainEventDispatcher domainEventDispatcher;
    public DbSet<Fellowship> Fellowships { get; set; }
    public DbSet<Hunting> Huntings { get; set; }

    public FellowshipDbContext(DbContextOptions<FellowshipDbContext> options,
        IDomainEventDispatcher domainEventDispatcher) : base(options)
    {
        this.domainEventDispatcher = domainEventDispatcher;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FellowshipDbContext).Assembly);

    async Task<int> IUnitOfWork.SaveChangesAsync()
    {
        var result = await SaveChangesAsync().ConfigureAwait(false);

        if (domainEventDispatcher is null)
        {
            return result;
        }

        var entitiesWithEvents = ChangeTracker
            .Entries()
            .Select(e => e.Entity as EntityBase)
            .Where(e => e?.DomainEvents is not null && e.DomainEvents.Any())
            .ToArray();

        await domainEventDispatcher.Dispatch(entitiesWithEvents);

        return result;
    }
}