using System.Linq.Expressions;
using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships.ValueObjects;
using BoardGame.WarOfTheRing.Fellowships.Domain.Base;

namespace BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships.Specifications;

public class RegionIsShadowControlled : Specification<Fellowship>
{
    private readonly Region region;

    public RegionIsShadowControlled(Region region)
    {
        this.region = region;
    }

    public override Expression<Func<Fellowship, bool>> ToExpression()
    {
        return _ => region.ControlledBy == RegionPlayer.Shadow;
    }
}