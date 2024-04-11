using BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps;
using BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoardGame.WarOfTheRing.Maps.Infrastructure.Persistence.EntityFrameworkCore.Maps.EntityConfigurations;

public class NationEntityConfiguration : IEntityTypeConfiguration<Nation>
{
    public void Configure(EntityTypeBuilder<Nation> builder)
    {
        builder.HasOne<Map>().WithMany(x => x.Nations).HasForeignKey(x => x.MapId);
        builder.ComplexProperty(x => x.BelongsTo, y => y.IsRequired());
        
        builder.OwnsMany(x => x.Reinforcements, unitBuilder =>
        {
            unitBuilder.ToJson();
            
            unitBuilder.OwnsOne(y => y.Type);
        });
    }
}