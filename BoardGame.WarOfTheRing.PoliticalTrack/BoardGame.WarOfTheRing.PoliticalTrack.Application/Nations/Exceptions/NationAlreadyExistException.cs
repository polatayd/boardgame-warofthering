namespace BoardGame.WarOfTheRing.PoliticalTrack.Application.Nations.Exceptions;

public class NationAlreadyExistException : ApplicationException
{
    public NationAlreadyExistException() : base("Nation is already exist")
    {
    }
}