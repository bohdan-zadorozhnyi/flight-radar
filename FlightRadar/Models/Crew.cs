using FlightRadar.Interfaces;
namespace FlightRadar.Models;

public class Crew : IBaseObject
{
    public string Type { get => "C"; set {} }
    public ulong ID { get; set; }
    public string Name { get; set; }
    public ulong Age { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public ushort Practice { get; set; }
    public string Role { get; set; }

    public override string ToString()
    {
        return $"Crew ID: {ID}, Name: {Name}, Age: {Age}, Phone: {Phone}, Email: {Email}, Practice: {Practice}, Role: {Role}";
    }
}