using BoardGame.WarOfTheRing.PoliticalTrack.Application.Nations;
using BoardGame.WarOfTheRing.PoliticalTrack.Domain.Aggregates;

namespace BoardGame.WarOfTheRing.PoliticalTrack.Infrastructure.Persistence.EntityFrameworkCore.Nations;

public class NationRepository(PoliticalTrackDbContext politicalTrackDbContext) : INationRepository
{
    public void Add(Nation nation)
    {
        politicalTrackDbContext.Nations.Add(nation);
    }

    public Nation Get(string name)
    {
        return politicalTrackDbContext.Nations.FirstOrDefault(x => x.Name.Value == name);
    }
}