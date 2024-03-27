using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships;
using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Hunts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoardGame.WarOfTheRing.Fellowships.Infrastructure.Persistence.EntityFrameworkCore.Hunts.EntityConfigurations;

public class HuntingEntityConfiguration : IEntityTypeConfiguration<Hunting>
{
    public void Configure(EntityTypeBuilder<Hunting> builder)
    {
        builder.HasOne<Fellowship>().WithOne().HasForeignKey<Hunting>(x => x.FellowshipId);
        builder.HasIndex(x => x.FellowshipId).IsUnique();
        builder.HasIndex(x => x.GameId).IsUnique();
        builder.ComplexProperty(x => x.HuntBox,y => y.IsRequired());
        builder.ComplexProperty(x => x.ActiveHunt,y =>
        {
            y.ComplexProperty(z => z.State, t => t.IsRequired());
            y.IsRequired();
        });
        builder.OwnsOne(x => x.HuntPool, huntingBuilder =>
        {
            huntingBuilder.ToJson();
            huntingBuilder.OwnsMany(z => z.HuntTiles, huntTilesBuilder =>
            {
                huntTilesBuilder.UsePropertyAccessMode(PropertyAccessMode.Field);
                huntTilesBuilder.ToJson();
            });
        });
    }
}