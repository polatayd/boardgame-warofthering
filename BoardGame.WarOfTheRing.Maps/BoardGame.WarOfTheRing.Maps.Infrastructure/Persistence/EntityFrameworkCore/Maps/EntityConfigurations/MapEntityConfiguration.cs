using BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps;
using BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoardGame.WarOfTheRing.Maps.Infrastructure.Persistence.EntityFrameworkCore.Maps.EntityConfigurations;

public class MapEntityConfiguration : IEntityTypeConfiguration<Map>
{
    public void Configure(EntityTypeBuilder<Map> builder)
    {
        builder.HasMany<Nation>().WithOne().HasForeignKey(x => x.MapId);
        builder.HasMany<Region>().WithOne().HasForeignKey(x => x.MapId);
    }
}