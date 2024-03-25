using BoardGame.WarOfTheRing.PoliticalTrack.Domain.Aggregates.Nations;

namespace BoardGame.WarOfTheRing.PoliticalTrack.Application.Nations;

public interface INationRepository
{
    public void Add(Nation nation);
    public Nation Get(string name, Guid gameId);
}