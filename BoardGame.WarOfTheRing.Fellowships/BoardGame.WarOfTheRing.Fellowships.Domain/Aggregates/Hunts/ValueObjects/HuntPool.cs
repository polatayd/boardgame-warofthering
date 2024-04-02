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
        return new HuntPool([
            HuntTile.CreateNumberedTile(3),
            HuntTile.CreateNumberedTile(3),
            HuntTile.CreateNumberedTile(3),
            HuntTile.CreateNumberedTile(2),
            HuntTile.CreateNumberedTile(2),
            HuntTile.CreateNumberedTile(1),
            HuntTile.CreateNumberedTile(1),
            HuntTile.CreateNumberedTile(0).WithRevealIcon(),
            HuntTile.CreateNumberedTile(0).WithRevealIcon(),
            HuntTile.CreateNumberedTile(2).WithRevealIcon(),
            HuntTile.CreateNumberedTile(1).WithRevealIcon(),
            HuntTile.CreateNumberedTile(1).WithRevealIcon(),
            HuntTile.CreateEyeTile().WithRevealIcon(),
            HuntTile.CreateEyeTile().WithRevealIcon(),
            HuntTile.CreateEyeTile().WithRevealIcon(),
            HuntTile.CreateEyeTile().WithRevealIcon()
        ]);
    }

    public Tuple<HuntTile, HuntPool> DrawHuntTile()
    {
        var newHuntTiles = huntTiles.Select(HuntTile.CreateFromHuntTile).ToList();
        if (newHuntTiles.Count == 0)
        {
            var reshuffledHuntPool = new HuntPool();
            return reshuffledHuntPool.DrawHuntTile();
        }

        var random = new Random();
        var huntTileIndex = random.Next(newHuntTiles.Count);
        var drawnHuntTile = newHuntTiles[huntTileIndex];
        newHuntTiles.RemoveAt(huntTileIndex);

        return new Tuple<HuntTile, HuntPool>(drawnHuntTile, new HuntPool(newHuntTiles));
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