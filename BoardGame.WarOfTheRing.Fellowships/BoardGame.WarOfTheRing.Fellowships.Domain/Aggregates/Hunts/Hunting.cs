using System.Diagnostics.CodeAnalysis;
using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Hunts.ValueObjects;
using BoardGame.WarOfTheRing.Fellowships.Domain.Base;

namespace BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Hunts;

public class Hunting : EntityBase, IAggregateRoot
{
    public Guid FellowshipId { get; private set; }
    public HuntBox HuntBox { get; private set; }
    public HuntPool HuntPool { get; private set; }
    
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    private Hunting() {}

    public Hunting(Guid fellowshipId)
    {
        Id = Guid.NewGuid();
        FellowshipId = fellowshipId;
        HuntBox = new HuntBox();
        HuntPool = new HuntPool();
    }
}