namespace BoardGame.WarOfTheRing.PoliticalTrack.Application;

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync();
}