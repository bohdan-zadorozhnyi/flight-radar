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
    
    public Crew(string[] values)
    {
        if (values.Length < 8)
            throw new ArgumentException("Invalid number of values for creating Crew object.");
        
        ID = ulong.Parse(values[1]);
        Name = values[2];
        Age = ulong.Parse(values[3]);
        Phone = values[4];
        Email = values[5];
        Practice = ushort.Parse(values[6]);
        Role = values[7];
    }

    public override string ToString()
    {
        return $"Crew ID: {ID}, Name: {Name}, Age: {Age}, Phone: {Phone}, Email: {Email}, Practice: {Practice}, Role: {Role}";
    }
}