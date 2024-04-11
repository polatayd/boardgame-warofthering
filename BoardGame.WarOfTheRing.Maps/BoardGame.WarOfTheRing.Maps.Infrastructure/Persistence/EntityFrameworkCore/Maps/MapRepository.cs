using BoardGame.WarOfTheRing.Maps.Application.Maps;
using BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps;
using Microsoft.EntityFrameworkCore;

namespace BoardGame.WarOfTheRing.Maps.Infrastructure.Persistence.EntityFrameworkCore.Maps;

public class MapRepository(MapDbContext fellowshipDbContext) : IMapRepository
{
    public void Add(Map map)
    {
        fellowshipDbContext.Maps.Add(map);
    }

    public Map Get()
    {
        return fellowshipDbContext.Maps
            .Include(x=>x.Nations)
            .Include(x=>x.Regions)
            .FirstOrDefault();
    }
}