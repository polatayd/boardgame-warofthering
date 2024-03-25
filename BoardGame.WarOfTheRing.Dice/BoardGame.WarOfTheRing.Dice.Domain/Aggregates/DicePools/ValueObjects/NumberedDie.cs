namespace BoardGame.WarOfTheRing.Dice.Domain.Aggregates.DicePools.ValueObjects;

public class NumberedDie : Die
{
    public static NumberedDie Create()
    {
        return new NumberedDie([
            DieFace.OneDieFace,
            DieFace.TwoDieFace,
            DieFace.ThreeDieFace,
            DieFace.FourDieFace,
            DieFace.FiveDieFace,
            DieFace.SixDieFace
        ]);
    }

    private NumberedDie(List<DieFace> dieFaces) : base(dieFaces)
    {
    }
}