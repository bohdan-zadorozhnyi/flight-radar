namespace FlightRadar.Utilities;
using Interfaces;
using Models;
using Mapsui.Projections;

public class FlightDataAdapter : IFlightDataAdapter
{
    private Dictionary<ulong, Airport> airportMap;
    
    public FlightsGUIData ConvertFlightDataToGUIFormat(List<IBaseObject> flightData)
    {
        airportMap = DataExtractor.InitializeAirportMap(flightData);
        
        var flights = DataExtractor.ExtractFlights(flightData);
        List<FlightGUI> flightGUIs = new List<FlightGUI>();
        DateTime now = DateTime.UtcNow;
        
        
        foreach (var flight in flights)
        {
            DateTime takeoffTime = flight.TakeoffTime;
            DateTime landingTime = flight.LandingTime;
            
            if (takeoffTime <= now && landingTime >= now)
            {
                if (airportMap.TryGetValue(flight.TargetID, out Airport target))
                {
                    var rotation = CalculateRotation(flight, target);
                    UpdateCurrentPosition(flight, target, now);
                    
                    flightGUIs.Add(new FlightGUI
                    {
                        ID = flight.ID,
                        WorldPosition = new WorldPosition
                            { Latitude = flight.Latitude, Longitude = flight.Longitude },
                        MapCoordRotation = rotation
                    });
                }
            }
        }

        return new FlightsGUIData(flightGUIs);
    }
    
    public void UpdateCurrentPosition(Flight flight, Airport target, DateTime currentTime)
    {
        float ratio = (float)(TimeSpan.FromSeconds(1).TotalSeconds / (flight.LandingTime - currentTime).TotalSeconds);
        flight.Longitude += ratio * (target.Longitude - flight.Longitude);
        flight.Latitude += ratio * (target.Latitude - flight.Latitude);
    }

    private double CalculateRotation(Flight flight, Airport target)
    {
        (double x, double y) tuple1 = SphericalMercator.FromLonLat(
            flight.Longitude,
            flight.Latitude
        );
        (double x, double y) tuple2 = SphericalMercator.FromLonLat(
            target.Longitude,
            target.Latitude
        );
        double num = Math.Atan2(tuple2.y - tuple1.y, tuple1.x - tuple2.x) - Math.PI / 2;

        return num;
    }
}
