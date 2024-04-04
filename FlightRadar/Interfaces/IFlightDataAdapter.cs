namespace FlightRadar.Interfaces;
using Models;

public interface IFlightDataAdapter
{
    FlightsGUIData ConvertFlightDataToGUIFormat(List<IBaseObject> flightData);
}