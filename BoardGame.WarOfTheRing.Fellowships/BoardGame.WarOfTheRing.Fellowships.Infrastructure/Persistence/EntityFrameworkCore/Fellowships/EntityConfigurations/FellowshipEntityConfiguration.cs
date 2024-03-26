using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoardGame.WarOfTheRing.Fellowships.Infrastructure.Persistence.EntityFrameworkCore.Fellowships.EntityConfigurations;

public class FellowshipEntityConfiguration : IEntityTypeConfiguration<Fellowship>
{
    public void Configure(EntityTypeBuilder<Fellowship> builder)
    {
        builder.HasIndex(x => x.GameId);
        builder.ComplexProperty(x => x.ProgressCounter);
        builder.ComplexProperty(x => x.CorruptionCounter);
    }
}