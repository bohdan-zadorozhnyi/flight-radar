namespace FlightRadar.Interfaces;

public interface IContactInfoUpdateable
{
    public string Phone { get; set; }
    public string Email { get; set; }
    void UpdateContactInfo(string phoneNumber, string emailAddress);
}