using System.Net;
using BoardGame.WarOfTheRing.PoliticalTrack.Domain.Aggregates;
using BoardGame.WarOfTheRing.PoliticalTrack.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoardGame.WarOfTheRing.PoliticalTrack.Infrastructure.Persistence.EntityFrameworkCore.Nations.EntityConfigurations;

public class NationEntityConfiguration : IEntityTypeConfiguration<Nation>
{
    public void Configure(EntityTypeBuilder<Nation> builder)
    {
        builder.Property(x => x.Name).HasConversion(x => x.Value, y => new Name(y));
        builder.Property(x => x.Position).HasConversion(x => x.Value, y => new Position(y));
        builder.Property(x => x.Status).HasConversion(x => x.IsActive, y => CreateStatus(y));
    }

    private static Status CreateStatus(bool b)
    {
        return b ? Status.Active : Status.Passive;
    }
}