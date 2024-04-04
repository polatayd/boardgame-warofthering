using System.Text.RegularExpressions;
using Serilog.Enrichers.Sensitive;

namespace BoardGame.WarOfTheRing.Fellowships.Api;

public class FellowshipMaskingOperator : RegexMaskingOperator
{
    private const string Pattern = ".*legolas.*";

    public FellowshipMaskingOperator() : base(Pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled)
    {
    }
}