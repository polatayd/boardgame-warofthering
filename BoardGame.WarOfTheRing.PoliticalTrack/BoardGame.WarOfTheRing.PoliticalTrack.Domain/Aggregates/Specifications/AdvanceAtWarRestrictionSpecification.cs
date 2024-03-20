using System.Linq.Expressions;
using BoardGame.WarOfTheRing.PoliticalTrack.Domain.Base;
using BoardGame.WarOfTheRing.PoliticalTrack.Domain.ValueObjects;

namespace BoardGame.WarOfTheRing.PoliticalTrack.Domain.Aggregates.Specifications;

public class AdvanceAtWarRestrictionSpecification : Specification<Nation>
{
    public override Expression<Func<Nation, bool>> ToExpression()
    {
        return nation => nation.Track.IsInOneStepBehindAtWarPosition() && nation.Status == Status.Passive;
    }
}