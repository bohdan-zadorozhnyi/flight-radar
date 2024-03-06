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
    public override string ToString()
    {
        return $"Cargo ID: {ID}, Weight: {Weight}, Code: {Code}, Description: {Description}";
    }
}