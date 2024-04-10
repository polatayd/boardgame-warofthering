using BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps;
using BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoardGame.WarOfTheRing.Maps.Infrastructure.Persistence.EntityFrameworkCore.Maps.EntityConfigurations;

public class RegionEntityConfiguration : IEntityTypeConfiguration<Region>
{
    public void Configure(EntityTypeBuilder<Region> builder)
    {
        builder.HasOne<Map>().WithMany(x => x.Regions).HasForeignKey(x => x.MapId);
        builder.HasOne(x => x.InBorderOf).WithMany();
        builder.ComplexProperty(x => x.Terrain, terrainBuilder =>
        {
            terrainBuilder.ComplexProperty(y => y.Settlement, settlementBuilder =>
            {
                settlementBuilder.ComplexProperty(z => z.ControlledBy, forceBuilder => forceBuilder.IsRequired());
                settlementBuilder.IsRequired();
            });
            terrainBuilder.IsRequired();
        });
        builder.OwnsOne(x => x.Army, armyBuilder =>
        {
            armyBuilder.ToJson();
            
            armyBuilder.OwnsMany(y => y.Units, unitBuilder =>
            {
                unitBuilder.UsePropertyAccessMode(PropertyAccessMode.Field);
                
                unitBuilder.OwnsOne(y => y.Type);
            });
        });
        
        builder.HasMany(x => x.NeighborRegions).WithMany()
            .UsingEntity(regionBuilder => regionBuilder.ToTable("RegionNeighborRegions"));
    }
}