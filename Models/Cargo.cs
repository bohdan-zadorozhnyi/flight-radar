using FlightRadar.Interfaces;
using FlightRadar.Utilities;
namespace FlightRadar.Models;

public class Cargo : IBaseObject
{
    public string Type { get => "CA"; set {} }
    public ulong ID { get; set; }
    public float Weight { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    
    public Cargo(string[] values)
    {
        if (values.Length < 5)
            throw new ArgumentException("Invalid number of values for creating Cargo object.");
        
        ID = ulong.Parse(values[1]);
        Weight = Parser.FloatParse(values[2]);
        Code = values[3];
        Description = values[4];
    }

    public override string ToString()
    {
        return $"Cargo ID: {ID}, Weight: {Weight}, Code: {Code}, Description: {Description}";
    }
}