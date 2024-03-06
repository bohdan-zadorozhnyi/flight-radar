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
    
    public CargoPlane(string[] values)
    {
        if (values.Length < 6)
            throw new ArgumentException("Invalid number of values for creating CargoPlane object.");
        
        ID = ulong.Parse(values[1]);
        Serial = values[2];
        CountryISO = values[3];
        Model = values[4];
        MaxLoad = Parser.FloatParse(values[5]);
    }

    public override string ToString()
    {
        return $"CargoPlane ID: {ID}, Serial: {Serial}, Country ISO: {CountryISO}, Model: {Model}, MaxLoad: {MaxLoad}";
    }
}