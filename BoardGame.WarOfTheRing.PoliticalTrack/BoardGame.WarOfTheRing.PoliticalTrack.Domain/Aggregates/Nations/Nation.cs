using System.Diagnostics.CodeAnalysis;
using BoardGame.WarOfTheRing.PoliticalTrack.Domain.Aggregates.Nations.Exceptions;
using BoardGame.WarOfTheRing.PoliticalTrack.Domain.Aggregates.Nations.Specifications;
using BoardGame.WarOfTheRing.PoliticalTrack.Domain.Aggregates.Nations.ValueObjects;
using BoardGame.WarOfTheRing.PoliticalTrack.Domain.Base;

namespace BoardGame.WarOfTheRing.PoliticalTrack.Domain.Aggregates.Nations;

public class Nation : EntityBase, IAggregateRoot
{
    public Status Status { get; private set; }
    public Position Position { get; private set; }
    public Name Name { get; private set; }
    public Guid GameId { get; private set; }

    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    private Nation() {}
    
    public Nation(Status status, Position position, Name name, Guid gameId)
    {
        if (status == Status.Passive && position.IsInAtWarPosition())
        {
            throw new ArgumentOutOfRangeException();
        }
        
        Status = status;
        Position = position;
        Name = name;
        GameId = gameId;
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