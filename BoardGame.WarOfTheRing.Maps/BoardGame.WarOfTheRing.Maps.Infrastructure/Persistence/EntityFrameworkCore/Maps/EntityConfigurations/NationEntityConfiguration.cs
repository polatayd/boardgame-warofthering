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

        builder.OwnsOne(x => x.Reinforcements, armyBuilder =>
        {
            armyBuilder.OwnsMany(x => x.Units, unitBuilder =>
            {
                unitBuilder.UsePropertyAccessMode(PropertyAccessMode.Field);
                unitBuilder.OwnsOne(y => y.Type);
                unitBuilder.Navigation(y => y.Type).IsRequired();
            });
            
            armyBuilder.OwnsMany(x => x.Leaders, leaderBuilder =>
            {
                leaderBuilder.UsePropertyAccessMode(PropertyAccessMode.Field);
            });
        });
        builder.Navigation(x => x.Reinforcements).IsRequired();
    }
}