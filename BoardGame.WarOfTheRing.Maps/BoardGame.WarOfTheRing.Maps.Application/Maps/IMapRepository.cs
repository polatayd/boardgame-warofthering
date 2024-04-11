using BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps;

namespace BoardGame.WarOfTheRing.Maps.Application.Maps;

public interface IMapRepository
{
    public void Add(Map map);
    public Map Get();
}