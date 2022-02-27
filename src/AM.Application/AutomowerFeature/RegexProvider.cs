using System.Text.RegularExpressions;

namespace AM.Application.AutomowerFeature;

public static class RegexProvider
{
    public static readonly Regex  LawnDimensions = new Regex(@"^[1-9]+\s[1-9]+$",
        RegexOptions.Compiled);
    
    public static readonly Regex  StartingPosition = new Regex(@"^\d+\s\d+\s[EWNS]{1}$",
        RegexOptions.Compiled); 
    
    public static readonly Regex  Instructions = new Regex(@"^[LRF]+$",
        RegexOptions.Compiled); 
     
}