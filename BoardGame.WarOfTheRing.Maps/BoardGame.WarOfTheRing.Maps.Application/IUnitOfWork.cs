namespace BoardGame.WarOfTheRing.Maps.Application;

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync();
}