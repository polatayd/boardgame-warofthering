using BoardGame.WarOfTheRing.Fellowships.Application.Hunts;
using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Hunts;

namespace BoardGame.WarOfTheRing.Fellowships.Infrastructure.Persistence.EntityFrameworkCore.Hunts;

public class HuntRepository(FellowshipDbContext fellowshipDbContext) : IHuntRepository
{
    public void Add(Hunting hunting)
    {
        fellowshipDbContext.Huntings.Add(hunting);
    }

    public Hunting GetByGameId(Guid gameId)
    {
        return fellowshipDbContext.Huntings.FirstOrDefault(x => x.GameId == gameId);
    }
    
    public Hunting GetById(Guid id)
    {
        return fellowshipDbContext.Huntings.FirstOrDefault(x => x.Id == id);
    }
}