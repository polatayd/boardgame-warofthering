using System.Diagnostics.CodeAnalysis;
using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships.DomainEvents;
using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships.ValueObjects;
using BoardGame.WarOfTheRing.Fellowships.Domain.Base;

namespace BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships;

public class Fellowship : EntityBase, IAggregateRoot
{
    public Guid GameId { get; private set; }
    public Guid HuntingId { get; private set; }
    public ProgressCounter ProgressCounter { get; private set; }
    public CorruptionCounter CorruptionCounter { get; private set; }
    
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    private Fellowship() {}

    private Fellowship(Guid gameId)
    {
        Id = Guid.NewGuid();
        GameId = gameId;
        HuntingId = Guid.NewGuid();
        ProgressCounter = new ProgressCounter();
        CorruptionCounter = new CorruptionCounter();
    }
    
    public static Fellowship Create(Guid gameId)
    {
        var fellowship = new Fellowship(gameId);

        fellowship.RegisterDomainEvent(new FellowshipCreated(fellowship.Id, fellowship.HuntingId));

        return fellowship;
    }

    public void ForwardProgressCounter()
    {
        ProgressCounter = ProgressCounter.Forward();
        
        RegisterDomainEvent(new FellowshipProgressCounterForwarded(Id, HuntingId, ProgressCounter.Value));
    }
}