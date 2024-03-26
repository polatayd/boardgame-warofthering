using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Hunts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BoardGame.WarOfTheRing.Fellowships.Infrastructure.Persistence.EntityFrameworkCore.Hunts.EntityConfigurations;

public class HuntingEntityConfiguration : IEntityTypeConfiguration<Hunting>
{
    public void Configure(EntityTypeBuilder<Hunting> builder)
    {
        builder.HasIndex(x => x.FellowshipId);
        builder.ComplexProperty(x => x.HuntBox);
        builder.ComplexProperty(x => x.HuntPool);
    }
}