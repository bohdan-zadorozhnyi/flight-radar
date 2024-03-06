namespace FlightRadar.Interfaces;

public interface IDataLoader
{
    List<IBaseObject> LoadData(string data);
}