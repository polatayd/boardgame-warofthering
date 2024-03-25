namespace BoardGame.WarOfTheRing.Dice.Domain.Aggregates.DicePools.ValueObjects;

public class ShadowActionDie : Die
{
    public static ShadowActionDie Create()
    {
        return new ShadowActionDie([
            DieFace.CharacterDieFace,
            DieFace.MusterArmyDieFace,
            DieFace.ArmyDieFace,
            DieFace.EventDieFace,
            DieFace.EyeDieFace,
            DieFace.MusterDieFace
        ]);
    }
    
    private ShadowActionDie(List<DieFace> dieFaces) : base(dieFaces)
    {
    }
}