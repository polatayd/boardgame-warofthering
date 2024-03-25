using BoardGame.WarOfTheRing.PoliticalTrack.Domain.Aggregates.Nations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoardGame.WarOfTheRing.PoliticalTrack.Infrastructure.Persistence.EntityFrameworkCore.Nations.
    EntityConfigurations;

public class NationEntityConfiguration : IEntityTypeConfiguration<Nation>
{
    public void Configure(EntityTypeBuilder<Nation> builder)
    {
        builder.ComplexProperty(x => x.Name, y => { y.IsRequired(); });
        builder.ComplexProperty(x => x.Position, y => y.IsRequired());
        builder.ComplexProperty(x => x.Status, y => y.IsRequired());
        builder.HasIndex(x => x.GameId);
    }
}