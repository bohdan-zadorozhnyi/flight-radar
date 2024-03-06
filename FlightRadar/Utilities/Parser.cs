using System.Globalization;
using FlightRadar.Models;
namespace FlightRadar.Utilities;

public class Parser
{
    public static float FloatParse(string floatValue)
    {
        if (string.IsNullOrEmpty(floatValue))
            return 0f;
        
        CultureInfo cultureInfo = CultureInfo.InvariantCulture;

        return float.Parse(floatValue, cultureInfo);
    }
    
    public static List<ulong> UlongListParse(string ids)
    {
        List<ulong> result = new List<ulong>();
        
        var idStrings = ids.Trim('[', ']').Split(';');
        foreach (var idString in idStrings)
        {
            result.Add(ulong.Parse(idString));
        }
        return result;
    }
}