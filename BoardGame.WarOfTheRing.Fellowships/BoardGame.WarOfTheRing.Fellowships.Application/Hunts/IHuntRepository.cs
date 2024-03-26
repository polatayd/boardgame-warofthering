using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Hunts;

namespace BoardGame.WarOfTheRing.Fellowships.Application.Hunts;

public interface IHuntRepository
{
    public void Add(Hunting hunting);
    public Hunting Get(Guid gameId);
}