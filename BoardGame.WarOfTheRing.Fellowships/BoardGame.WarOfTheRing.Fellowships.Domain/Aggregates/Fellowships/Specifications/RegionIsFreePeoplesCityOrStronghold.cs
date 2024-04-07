using System.Linq.Expressions;
using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships.ValueObjects;
using BoardGame.WarOfTheRing.Fellowships.Domain.Base;

namespace BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships.Specifications;

public class RegionIsFreePeoplesCityOrStronghold : Specification<Fellowship>
{
    private readonly Region region;

    public RegionIsFreePeoplesCityOrStronghold(Region region)
    {
        this.region = region;
    }

    public override Expression<Func<Fellowship, bool>> ToExpression()
    {
        return _ => region.OwnedBy == RegionPlayer.FreePeoples &&
                    (region.Type == RegionType.City || region.Type == RegionType.Stronghold);
    }
}