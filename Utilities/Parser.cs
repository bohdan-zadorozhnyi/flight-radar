namespace FlightRadar.Utilities;

public class Parser
{
    public static float FloatParse(string floatValue)
    {
        if (string.IsNullOrEmpty(floatValue))
            return 0f;
        
        floatValue = floatValue.Replace('.', ',');

        return float.Parse(floatValue);
    }
    
    public static ulong[] UlongArrayParse(string ids)
    {
        if (string.IsNullOrEmpty(ids))
            return new ulong[0];

        var idStrings = ids.Trim('[', ']').Split(';');
        var result = new ulong[idStrings.Length];
        for (int i = 0; i < idStrings.Length; i++)
        {
            result[i] = ulong.Parse(idStrings[i]);
        }
        return result;
    }
}