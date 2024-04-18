using FlightRadar.Interfaces;
namespace FlightRadar.Models;

public class Crew : IBaseObject, IIDUpdateable, IContactInfoUpdateable
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