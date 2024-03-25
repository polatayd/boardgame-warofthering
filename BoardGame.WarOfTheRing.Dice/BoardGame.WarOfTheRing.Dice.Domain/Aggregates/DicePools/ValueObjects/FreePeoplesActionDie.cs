namespace BoardGame.WarOfTheRing.Dice.Domain.Aggregates.DicePools.ValueObjects;

public class FreePeoplesActionDie : Die
{
    public static FreePeoplesActionDie Create()
    {
        return new FreePeoplesActionDie([
            DieFace.CharacterDieFace,
            DieFace.CharacterDieFace,
            DieFace.MusterArmyDieFace,
            DieFace.WillOfTheWestDieFace,
            DieFace.MusterDieFace,
            DieFace.EventDieFace
        ]);
    }
    
    private FreePeoplesActionDie(List<DieFace> dieFaces) : base(dieFaces)
    {
    }
}