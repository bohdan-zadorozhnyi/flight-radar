using FlightRadar.Interfaces;
namespace FlightRadar.Models;

public class Passenger : IBaseObject, IIDUpdateable, IContactInfoUpdateable, ILoad
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
    
    public void UpdateID(ulong newID)
    {
        this.ID = newID;
    }
    
    public void UpdateContactInfo(string phoneNumber, string emailAddress)
    {
        this.Phone = phoneNumber;
        this.Email = emailAddress;
    }
}