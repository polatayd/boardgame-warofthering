namespace BoardGame.WarOfTheRing.Fellowships.Application.Services;

public interface IDiceService
{
    public Task<IReadOnlyList<int>> SendRollDiceRequestAsync(int numberOfDice);
}