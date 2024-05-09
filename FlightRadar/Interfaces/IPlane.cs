namespace FlightRadar.Interfaces;

public interface IPlane
{
    public ulong ID { get; set; }
    public string Serial { get; set; }
    public string CountryISO { get; set; }
    public string Model { get; set; }
}