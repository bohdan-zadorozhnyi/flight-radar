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
    public static List<IPlane> ExtractPlanes(List<IBaseObject> objects)
    {
        return objects.OfType<IPlane>().ToList();
    }
    
    public static List<IReportable> ExtractReportables(List<IBaseObject> flightData)
    {
        return flightData.OfType<IReportable>().ToList();
    }
    
    public static IEnumerable<IBaseObject> ExtractDataByType(string objectClass, List<IBaseObject> data)
    {
        Dictionary<string, Func<IEnumerable<IBaseObject>>> _typeFactories = new Dictionary<string, Func<IEnumerable<IBaseObject>>>
        {
            { "airport", () => data.OfType<Airport>() },
            { "cargo", () => data.OfType<Cargo>() },
            { "cargoplane", () => data.OfType<CargoPlane>() },
            { "crew", () => data.OfType<Crew>() },
            { "flight", () => data.OfType<Flight>() },
            { "passenger", () => data.OfType<Passenger>() },
            { "passengerplane", () => data.OfType<PassengerPlane>() }
        };
        
        string key = objectClass.ToLower();
        if (_typeFactories.TryGetValue(key, out var factory))
        {
            return factory();
        }
        
        return Enumerable.Empty<IBaseObject>();
    }
    
    public static List<Crew> FetchCrewMembersByIds(List<ulong> crewIds, List<IBaseObject> dataList)
    {
        List<Crew> crewMembers = new List<Crew>();
        var crewMap = dataList.OfType<Crew>().ToDictionary(crew => crew.ID, crew => crew);

        foreach (ulong crewId in crewIds)
        {
            if (crewMap.TryGetValue(crewId, out Crew crewMember))
            {
                crewMembers.Add(crewMember);
            }
            else
            {
                Console.WriteLine($"No crew member found for ID {crewId}");
            }
        }

        return crewMembers;
    }
    
    public static List<ILoad> FetchLoadsByIds(List<ulong> loadIds, List<IBaseObject> dataList)
    {
        List<ILoad> loads = new List<ILoad>();
        var loadMap = dataList.OfType<ILoad>().ToDictionary(load => load.ID, load => load);

        foreach (ulong loadId in loadIds)
        {
            if (loadMap.TryGetValue(loadId, out ILoad load))
            {
                loads.Add(load);
            }
            else
            {
                Console.WriteLine($"No load found for ID {loadId}");
            }
        }

        return loads;
    }
    
    public static Dictionary<ulong, Airport> InitializeAirportMap(List<IBaseObject> snapshot)
    {
        Dictionary<ulong, Airport> airportMap = new Dictionary<ulong, Airport>(); 
        var airports = DataExtractor.ExtractAirports(snapshot);
        
        foreach (var airport in airports)
        {
            if (airportMap.ContainsKey(airport.ID))
            {
                airportMap[airport.ID] = airport;
            }
            else
            {
                airportMap.Add(airport.ID, airport);
            }
        }

        return airportMap;
    }
}