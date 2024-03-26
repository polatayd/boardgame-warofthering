using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships;

namespace BoardGame.WarOfTheRing.Fellowships.Application.Fellowships;

public interface IFellowshipRepository
{
    public void Add(Fellowship fellowship);
    public Fellowship Get(Guid gameId);
}