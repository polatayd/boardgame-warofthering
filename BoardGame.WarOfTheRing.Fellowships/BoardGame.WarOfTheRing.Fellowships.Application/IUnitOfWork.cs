namespace BoardGame.WarOfTheRing.Fellowships.Application;

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync();
}