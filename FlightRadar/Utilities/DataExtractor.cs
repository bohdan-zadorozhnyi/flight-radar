using FlightRadar.Models;
using FlightRadar.Interfaces;
namespace FlightRadar.Utilities;

public static class DataExtractor
{
    public static List<Flight> ExtractFlights(List<IBaseObject> objects)
    {
        List<Flight> flights = new List<Flight>();

        foreach (var obj in objects)
        {
            if (obj is Flight flight)
            {
                flights.Add(flight);
            }
        }

        return flights;
    }

    public static List<Airport> ExtractAirports(List<IBaseObject> objects)
    {
        List<Airport> airports = new List<Airport>();

        foreach (var obj in objects)
        {
            if (obj is Airport airport)
            {
                airports.Add(airport);
            }
        }

        return airports;
    }
}