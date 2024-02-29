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
    
    public Passenger(string[] values)
    {
        if (values.Length < 8)
            throw new ArgumentException("Invalid number of values for creating Passenger object.");
        
        ID = ulong.Parse(values[1]);
        Name = values[2];
        Age = ulong.Parse(values[3]);
        Phone = values[4];
        Email = values[5];
        Class = values[6];
        Miles = ulong.Parse(values[7]);
    }

    public override string ToString()
    {
        return $"Passenger ID: {ID}, Name: {Name}, Age: {Age}, Phone: {Phone}, Email: {Email}, Class: {Class}, Miles: {Miles}";
    }
}