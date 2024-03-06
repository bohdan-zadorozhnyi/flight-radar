using FlightRadar.Interfaces;
namespace FlightRadar.Models;

public class Passenger : IBaseObject
{
    public string Type { get => "P"; set {} }
    public ulong ID { get; set; }
    public string Name { get; set; }
    public ulong Age { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Class { get; set; }
    public ulong Miles { get; set; }

    public override string ToString()
    {
        return $"Passenger ID: {ID}, Name: {Name}, Age: {Age}, Phone: {Phone}, Email: {Email}, Class: {Class}, Miles: {Miles}";
    }
}