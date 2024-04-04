namespace FlightRadar.Utilities;
using FlightTrackerGUI;
using Interfaces;

public class FlightRadarGUIRunner
{
    private List<IBaseObject> _flightData;
    private IFlightDataAdapter _adapter;

    public FlightRadarGUIRunner(List<IBaseObject> flightData)
    {
        lock (flightData)
        {
            this._flightData = flightData;
        }

        _adapter = new FlightDataAdapter();
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
        var flightsGUIData = FlightDataToGUIFormat();
        
        Runner.UpdateGUI(flightsGUIData);
    }
    
    private FlightsGUIData FlightDataToGUIFormat()
    {
        List<IBaseObject> snapshot;
        lock (_flightData)
        {
            snapshot = new List<IBaseObject>(_flightData);
        }
        
        var flightsGUIData = _adapter.ConvertFlightDataToGUIFormat(snapshot);

        return flightsGUIData;
    }
}       