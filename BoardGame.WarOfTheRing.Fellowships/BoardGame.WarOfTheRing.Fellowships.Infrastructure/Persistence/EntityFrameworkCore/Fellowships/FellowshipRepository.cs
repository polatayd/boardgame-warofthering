using BoardGame.WarOfTheRing.Fellowships.Application.Fellowships;
using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships;

namespace BoardGame.WarOfTheRing.Fellowships.Infrastructure.Persistence.EntityFrameworkCore.Fellowships;

public class FellowshipRepository(FellowshipDbContext fellowshipDbContext) : IFellowshipRepository
{
    public void Add(Fellowship fellowship)
    {
        fellowshipDbContext.Fellowships.Add(fellowship);
    }

    public Fellowship Get(Guid gameId)
    {
        return fellowshipDbContext.Fellowships.FirstOrDefault(x => x.GameId == gameId);
    }
}