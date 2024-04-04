namespace FlightRadar.Models;
using Interfaces;

public class CargoPlane : IBaseObject, IReportable
{
    public string Type { get => "CP"; set {} }
    public ulong ID { get; set; }
    public string Serial { get; set; }
    public string CountryISO { get; set; }
    public string Model { get; set; }
    public float MaxLoad { get; set; }
    public override string ToString()
    {
        return $"CargoPlane ID: {ID}, Serial: {Serial}, Country ISO: {CountryISO}, Model: {Model}, MaxLoad: {MaxLoad}";
    }
    
    public string Accept(INewsProvider visitor) => visitor.VisitCargoPlane(this);
}