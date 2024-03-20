using BoardGame.WarOfTheRing.PoliticalTrack.Application.Nations.Inputs;
using BoardGame.WarOfTheRing.PoliticalTrack.Application.Nations.Outputs;
using BoardGame.WarOfTheRing.PoliticalTrack.Domain.ValueObjects;

namespace BoardGame.WarOfTheRing.PoliticalTrack.Application.Nations.Factories;

public static class NationFactory
{
    public static Status CreateStatus(StatusInput statusInput)
    {
        return statusInput switch
        {
            StatusInput.Active => Status.Active,
            StatusInput.Passive => Status.Passive,
            _ => throw new ArgumentOutOfRangeException(nameof(statusInput), statusInput, null)
        };
    }
    
    public static StatusOutput CreateStatusOutput(Status status)
    {
        if (status == Status.Active)
            return StatusOutput.Active;

        if (status == Status.Passive)
            return StatusOutput.Passive;

        throw new ArgumentOutOfRangeException(nameof(status), status, null);
    }
}