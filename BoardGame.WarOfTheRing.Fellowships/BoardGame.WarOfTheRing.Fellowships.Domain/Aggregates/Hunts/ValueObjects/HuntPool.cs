using BoardGame.WarOfTheRing.Fellowships.Domain.Base;

namespace BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Hunts.ValueObjects;

public class HuntPool : ValueObject
{
    private readonly List<HuntTile> huntTiles = new();
    public IReadOnlyList<HuntTile> HuntTiles => huntTiles.AsReadOnly();

    private HuntPool()
    {
    }

    private HuntPool(List<HuntTile> huntTiles)
    {
        this.huntTiles = huntTiles;
    }
    
    public static HuntPool Create()
    {
        //TODO: ALl tiles should be initialized, these are just a few of them.
        return new HuntPool([
            new HuntTile(1, true, false, false, false),
            new HuntTile(2, false, false, false, false),
            new HuntTile(0, false, false, true, false)
        ]);
    }

    public Tuple<HuntTile, HuntPool> DrawHuntTile()
    {
        if (huntTiles.Count == 0)
        {
            var reshuffledHuntPool = new HuntPool();
            return reshuffledHuntPool.DrawHuntTile();
        }

        var random = new Random();
        var huntTileIndex = random.Next(huntTiles.Count);
        var drawnHuntTile = huntTiles[huntTileIndex];
        huntTiles.RemoveAt(huntTileIndex);

        return new Tuple<HuntTile, HuntPool>(drawnHuntTile, new HuntPool(huntTiles));
    }

    public HuntPool AddHuntTile(HuntTile huntTile)
    {
        huntTiles.Add(huntTile);

        return new HuntPool(huntTiles);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        return huntTiles;
    }
}