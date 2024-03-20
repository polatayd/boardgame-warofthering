using BoardGame.WarOfTheRing.PoliticalTrack.Domain.Aggregates.Exceptions;
using BoardGame.WarOfTheRing.PoliticalTrack.Domain.Aggregates.Specifications;
using BoardGame.WarOfTheRing.PoliticalTrack.Domain.Base;
using BoardGame.WarOfTheRing.PoliticalTrack.Domain.ValueObjects;

namespace BoardGame.WarOfTheRing.PoliticalTrack.Domain.Aggregates;

public class Nation : EntityBase, IAggregateRoot
{
    public Nation(Status status, Track track)
    {
        if (status == Status.Passive && track.IsInAtWarPosition())
        {
            throw new ArgumentOutOfRangeException();
        }
        
        Status = status;
        Track = track;
    }

    public Status Status { get; private set; }
    public Track Track { get; private set; }

    public bool IsAtWar()
    {
        return Track.IsInAtWarPosition();
    }

    public void AdvanceOnPoliticalTrack()
    {
        var restriction = new AdvanceAtWarRestrictionSpecification();
        
        if (restriction.IsSatisfiedBy(this))
        {
            throw new PoliticalTrackAdvanceException(PoliticalTrackAdvanceException.Reason.BecauseOfPassiveStatus);
        }
        
        Track = Track.AdvancePosition();
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