namespace FlightRadar.Utilities;
using System.Globalization;

public class Converter
{
    public static string ISOToCountryName(string isoCode)
    {
        var cultureInfos = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

        foreach (var ci in cultureInfos)
        {
            var region = new RegionInfo(ci.Name);
            if (region.ThreeLetterISORegionName.Equals(isoCode, StringComparison.InvariantCultureIgnoreCase))
            {
                return region.EnglishName;
            }
        }

        return "Unknown Country";
    }
}