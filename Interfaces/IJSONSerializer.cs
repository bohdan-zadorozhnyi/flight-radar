namespace FlightRadar.Interfaces;

public interface IJSONSerializer
{
    string Serialize(List<IBaseObject> objects);
}