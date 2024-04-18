namespace FlightRadar.Utilities;
using Interfaces;
using Models;

public class DataUpdater
{
    
    public static void UpdateFlights(List<IBaseObject> dataList)
    {
        foreach (var flight in dataList.OfType<Flight>().ToList())
        {
            flight.CrewMembers = DataExtractor.FetchCrewMembersByIds(flight.CrewIDs, dataList);
            flight.Loads = DataExtractor.FetchLoadsByIds(flight.LoadIDs, dataList);
        }
    }
    
    public static void UpdateFlightCrewId(List<IBaseObject> dataList, ulong oldID, ulong newID)
    {
        foreach (var flight in dataList.OfType<Flight>())
        {
            if (flight.CrewIDs.Contains(oldID))
            {
                while(flight.CrewIDs.Contains(oldID))
                {
                    flight.CrewIDs.Remove(oldID);
                    flight.CrewIDs.Add(newID);
                }
                flight.CrewMembers = DataExtractor.FetchCrewMembersByIds(flight.CrewIDs, dataList);
            }
        }
    }
    
    public static void UpdateFlightLoadId(List<IBaseObject> dataList, ulong oldID, ulong newID)
    {
        foreach (var flight in dataList.OfType<Flight>())
        {
            if (flight.LoadIDs.Contains(oldID))
            {
                while (flight.LoadIDs.Contains(oldID))
                {
                    flight.LoadIDs.Remove(oldID);
                    flight.LoadIDs.Add(newID);
                }
                flight.Loads = DataExtractor.FetchLoadsByIds(flight.LoadIDs, dataList);
            }
        }
    }
    
    public static void UpdateFlightPlaneID(List<IBaseObject> dataList, ulong oldPlaneID, ulong newPlaneID)
    {
        foreach (var flight in dataList.OfType<Flight>())
        {
            if (flight.PlaneID == oldPlaneID)
            {
                flight.PlaneID = newPlaneID;
                Console.WriteLine($"Updated Plane ID from {oldPlaneID} to {newPlaneID} in Flight ID {flight.ID}");
            }
        }
    }
    
    public static void UpdateFlightsInitialPosition(List<IBaseObject> dataList)
    {
        Dictionary<ulong, Airport> airportMap = DataExtractor.InitializeAirportMap(dataList);
        
        foreach (var flight in dataList.OfType<Flight>())
        {
            airportMap.TryGetValue(flight.OriginID, out Airport origin);
            airportMap.TryGetValue(flight.TargetID, out Airport target);
            
            DateTime currentTime = DateTime.UtcNow;
            double fractionOfJourneyCompleted =
                (currentTime - flight.TakeoffTime).TotalSeconds / (flight.LandingTime - flight.TakeoffTime).TotalSeconds;

            flight.Latitude = (float)(origin.Latitude +
                                      (target.Latitude - origin.Latitude) * fractionOfJourneyCompleted);
            flight.Longitude = (float) (origin.Longitude + (target.Longitude - origin.Longitude) * fractionOfJourneyCompleted);
        }
    }
}