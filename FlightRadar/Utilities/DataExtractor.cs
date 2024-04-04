using FlightRadar.Models;
using FlightRadar.Interfaces;
namespace FlightRadar.Utilities;

public static class DataExtractor
{
    public static List<Flight> ExtractFlights(List<IBaseObject> objects)
    {
        return objects.OfType<Flight>().ToList();
    }

    public static List<Airport> ExtractAirports(List<IBaseObject> objects)
    {
        return objects.OfType<Airport>().ToList();
    }
    
    public static List<IReportable> ExtractReportables(List<IBaseObject> flightData)
    {
        return flightData.OfType<IReportable>().ToList();
    }
}