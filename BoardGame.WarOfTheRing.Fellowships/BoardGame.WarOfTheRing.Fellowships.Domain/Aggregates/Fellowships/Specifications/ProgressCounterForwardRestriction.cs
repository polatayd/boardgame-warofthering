using System.Linq.Expressions;
using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Hunts.ValueObjects;
using BoardGame.WarOfTheRing.Fellowships.Domain.Base;

namespace BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships.Specifications;

public class ProgressCounterForwardRestriction : Specification<Fellowship>
{
    public string Message { get; private set; }
    private readonly HuntState huntState;

    public ProgressCounterForwardRestriction(HuntState huntState)
    {
        this.huntState = huntState;
        Message = "Progress counter can not be forwarded when there is an active hunt";
    }

    public override Expression<Func<Fellowship, bool>> ToExpression()
    {
        return _ => huntState != HuntState.Empty;
    }
}