namespace FlightRadar.Utilities;
using Interfaces;
using Models;
using Mapsui.Projections;

public class FlightDataAdapter : IFlightDataAdapter
{
    private Dictionary<ulong, Airport> airportMap = new Dictionary<ulong, Airport>();
    
    public FlightsGUIData ConvertFlightDataToGUIFormat(List<IBaseObject> flightData)
    {
        InitializeAirportMap(flightData);
        
        var flights = DataExtractor.ExtractFlights(flightData);
        List<FlightGUI> flightGUIs = new List<FlightGUI>();
        DateTime now = DateTime.UtcNow;
        
        foreach (var flight in flights)
        {
            DateTime takeoffTime = flight.TakeoffTime;
            DateTime landingTime = flight.LandingTime;
            
            if (takeoffTime <= now && landingTime >= now)
            {
                if (airportMap.TryGetValue(flight.OriginID, out Airport origin) &&
                    airportMap.TryGetValue(flight.TargetID, out Airport target))
                {
                    var currentPosition = CalculateCurrentPosition(origin, target, takeoffTime, landingTime, now);
                    var rotation = CalculateRotation(origin, target);
                    flightGUIs.Add(new FlightGUI
                    {
                        ID = flight.ID,
                        WorldPosition = new WorldPosition
                            { Latitude = currentPosition.lat, Longitude = currentPosition.lon },
                        MapCoordRotation = rotation
                    });
                }
            }
        }

        return new FlightsGUIData(flightGUIs);
    }

    private (double lat, double lon) CalculateCurrentPosition(Airport origin, Airport target, DateTime takeoffTime,
        DateTime landingTime, DateTime currentTime)
    {
        double fractionOfJourneyCompleted =
            (currentTime - takeoffTime).TotalSeconds / (landingTime - takeoffTime).TotalSeconds;
        
        double lat = origin.Latitude + (target.Latitude - origin.Latitude) * fractionOfJourneyCompleted;
        double lon = origin.Longitude + (target.Longitude - origin.Longitude) * fractionOfJourneyCompleted;

        return (lat, lon);
    }

    private double CalculateRotation(Airport origin, Airport target)
    {
        (double x, double y) tuple1 = SphericalMercator.FromLonLat(
            origin.Longitude,
            origin.Latitude
        );
        (double x, double y) tuple2 = SphericalMercator.FromLonLat(
            target.Longitude,
            target.Latitude
        );
        double num = Math.Atan2(tuple2.y - tuple1.y, tuple1.x - tuple2.x) - Math.PI / 2;

        return num;
    }
    
    private void InitializeAirportMap(List<IBaseObject> snapshot)
    {
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
    }
}
