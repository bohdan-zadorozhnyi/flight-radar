namespace FlightRadar.Utilities;
using Models;

public class PositionDecorator
{
    private Flight _flight;
    private Dictionary<ulong, Airport> _airportMap;
    private (double lat, double lon)? _currentUpdatedPosition = null;

    public PositionDecorator(Flight flight, Dictionary<ulong, Airport> airportMap)
    {
        _flight = flight;
        _airportMap = airportMap;
    }

    public void HandlePositionUpdate(double updatedLat, double updatedLon)
    {
        _currentUpdatedPosition = (updatedLat, updatedLon);
    }

    public (double lat, double lon) CalculateCurrentPosition(DateTime currentTime)
    {
        var origin = _currentUpdatedPosition ?? GetAirportPosition(_flight.OriginID);
        var target = GetAirportPosition(_flight.TargetID);
        DateTime takeoffTime = _flight.TakeoffTime;
        DateTime landingTime = _flight.LandingTime;

        double fractionOfJourneyCompleted = (currentTime - takeoffTime).TotalSeconds / (landingTime - takeoffTime).TotalSeconds;
        double lat = origin.lat + (target.lat - origin.lat) * fractionOfJourneyCompleted;
        double lon = origin.lon + (target.lon - origin.lon) * fractionOfJourneyCompleted;
        
        return (lat, lon);
    }

    private (double lat, double lon) GetAirportPosition(ulong airportId)
    {
        if (_airportMap.TryGetValue(airportId, out Airport airport))
        {
            return (airport.Latitude, airport.Longitude);
        }
        throw new KeyNotFoundException($"No airport found with ID {airportId}");
    }
}
