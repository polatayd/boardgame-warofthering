using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoardGame.WarOfTheRing.Fellowships.Infrastructure.Persistence.EntityFrameworkCore.Fellowships.EntityConfigurations;

public class FellowshipEntityConfiguration : IEntityTypeConfiguration<Fellowship>
{
    public void Configure(EntityTypeBuilder<Fellowship> builder)
    {
        builder.HasIndex(x => x.GameId).IsUnique();
        builder.ComplexProperty(x => x.ProgressCounter, y => y.IsRequired());
        builder.ComplexProperty(x => x.CorruptionCounter, y=>y.IsRequired());
        builder.OwnsMany(x => x.Characters, fellowshipBuilder =>
        {
            fellowshipBuilder.ToJson();
        });
        builder.Ignore(x => x.Guide);
    }
}