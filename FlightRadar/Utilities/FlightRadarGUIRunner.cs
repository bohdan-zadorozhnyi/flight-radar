using FlightTrackerGUI;
using FlightRadar.Interfaces;
using System.Globalization;
using FlightRadar.Models;
using Mapsui.Projections;
namespace FlightRadar.Utilities;

public class FlightRadarGUIRunner
{
    private List<IBaseObject> _flightData;
    private Dictionary<ulong, Airport> _airportMap;

    public FlightRadarGUIRunner(List<IBaseObject> flightData)
    {
        lock (flightData)
        {
            this._flightData = flightData;
        }

        _airportMap = new Dictionary<ulong, Airport>();
        InitializeAirportMap();
    }

    public void RunInterface()
    {
        StartFlightDataUpdates();
        
        Runner.Run();
    }

    private void StartFlightDataUpdates()
    {
        Thread updateThread = new Thread(async () =>
        {
            PeriodicTimer timer = new PeriodicTimer(TimeSpan.FromSeconds(1));
            while (await timer.WaitForNextTickAsync())
            {
                UpdateFlightsGUIData();
            }
        })
        {
            IsBackground = true
        };

        updateThread.Start();
    }

    private void UpdateFlightsGUIData()
    {
        InitializeAirportMap();

        var flightsGUIData = ConvertFlightDataToGUIFormat();
        
        Runner.UpdateGUI(flightsGUIData);
    }

    private void InitializeAirportMap()
    {
        List<IBaseObject> snapshot;
        lock (_flightData)
        {
            snapshot = new List<IBaseObject>(_flightData);
        }

        var airports = DataExtractor.ExtractAirports(snapshot);
        
        foreach (var airport in airports)
        {
            if (_airportMap.ContainsKey(airport.ID))
            {
                _airportMap[airport.ID] = airport;
            }
            else
            {
                _airportMap.Add(airport.ID, airport);
            }
        }
    }
    
    private FlightsGUIData ConvertFlightDataToGUIFormat()
    {
        List<IBaseObject> snapshot;
        lock (_flightData)
        {
            snapshot = new List<IBaseObject>(_flightData);
        }

        var flights = DataExtractor.ExtractFlights(snapshot);

        List<FlightGUI> flightGUIs = new List<FlightGUI>();
        DateTime now = DateTime.UtcNow;
        
        foreach (var flight in flights)
        {
            DateTime takeoffTime = flight.TakeoffTime;
            DateTime landingTime = flight.LandingTime;

            if (takeoffTime <= now && landingTime >= now)
            {
                if (_airportMap.TryGetValue(flight.OriginID, out Airport origin) &&
                    _airportMap.TryGetValue(flight.TargetID, out Airport target))
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
        double num = Math.Atan2(tuple2.y - tuple1.y, tuple1.x - tuple2.x) + Math.PI / 2;
        if (num >= 0.0)
        {
            num += Math.PI;
        }

        return num;
    }
}       