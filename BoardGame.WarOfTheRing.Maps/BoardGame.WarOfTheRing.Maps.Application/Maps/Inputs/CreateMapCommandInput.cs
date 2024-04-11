namespace BoardGame.WarOfTheRing.Maps.Application.Maps.Inputs;

public class CreateMapCommandInput
{
    public CreateMapCommandInput(CreateNationsCommandInput createNationsCommandInput, CreateRegionsCommandInput createRegionsCommandInput)
    {
        CreateNationsCommandInput = createNationsCommandInput;
        CreateRegionsCommandInput = createRegionsCommandInput;
    }

    public CreateNationsCommandInput CreateNationsCommandInput { get; set; }
    public CreateRegionsCommandInput CreateRegionsCommandInput { get; set; }
}