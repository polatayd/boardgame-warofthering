using System.Linq.Expressions;
using BoardGame.WarOfTheRing.PoliticalTrack.Domain.Aggregates.Nations.ValueObjects;
using BoardGame.WarOfTheRing.PoliticalTrack.Domain.Base;

namespace BoardGame.WarOfTheRing.PoliticalTrack.Domain.Aggregates.Nations.Specifications;

public class AdvanceAtWarRestrictionSpecification : Specification<Nation>
{
    public override Expression<Func<Nation, bool>> ToExpression()
    {
        return nation => nation.Position.IsInOneStepBehindAtWarPosition() && nation.Status == Status.Passive;
    }
}