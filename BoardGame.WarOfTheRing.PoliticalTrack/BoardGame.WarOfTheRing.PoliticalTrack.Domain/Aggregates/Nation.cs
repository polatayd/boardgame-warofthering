using BoardGame.WarOfTheRing.PoliticalTrack.Domain.Aggregates.Exceptions;
using BoardGame.WarOfTheRing.PoliticalTrack.Domain.Aggregates.Specifications;
using BoardGame.WarOfTheRing.PoliticalTrack.Domain.Base;
using BoardGame.WarOfTheRing.PoliticalTrack.Domain.ValueObjects;

namespace BoardGame.WarOfTheRing.PoliticalTrack.Domain.Aggregates;

public class Nation : EntityBase, IAggregateRoot
{
    public Status Status { get; private set; }
    public Position Position { get; private set; }
    public Name Name { get; private set; }

    private Nation() {}
    
    public Nation(Status status, Position position, Name name)
    {
        if (status == Status.Passive && position.IsInAtWarPosition())
        {
            throw new ArgumentOutOfRangeException();
        }
        
        Status = status;
        Position = position;
        Name = name;
    }

    public bool IsAtWar()
    {
        return Position.IsInAtWarPosition();
    }

    public void AdvanceOnPoliticalTrack()
    {
        var restriction = new AdvanceAtWarRestrictionSpecification();
        
        if (restriction.IsSatisfiedBy(this))
        {
            throw new PoliticalTrackAdvanceException(PoliticalTrackAdvanceException.Reason.BecauseOfPassiveStatus);
        }
        
        Position = Position.AdvancePosition();
    }

    public void Activate()
    {
        if (Status == Status.Active)
        {
            return;
        }

        Status = Status.Active;
    }
}