using FlightRadar.Interfaces;
using FlightRadar.Utilities;
namespace FlightRadar.Models;

public class CargoPlane : IBaseObject
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
}