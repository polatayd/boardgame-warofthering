using BoardGame.WarOfTheRing.PoliticalTrack.Domain.Aggregates;

namespace BoardGame.WarOfTheRing.PoliticalTrack.Application.Nations;

public interface INationRepository
{
    public void Add(Nation nation);
    public Nation Get(string name);
}