namespace FlightRadar.Interfaces;

public interface IDataFactory
{
    IBaseObject CreateObject(string[] values);
}