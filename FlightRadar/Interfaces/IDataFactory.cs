namespace FlightRadar.Interfaces;

public interface IDataFactory
{
    IBaseObject CreateObject(object values);
}