namespace BoardGame.WarOfTheRing.Fellowships.Application.Services;

public interface IDiceService
{
    public Task<List<int>> SendRollDiceRequestAsync(int numberOfDice);
}