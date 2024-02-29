namespace FlightRadar.Interfaces;

public interface IBaseObject
{
    string Type { get; set; }
    ulong ID { get; set; }
    string ToString();
}