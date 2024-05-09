namespace FlightRadar.Models;
using Interfaces;
using Structures;

public class Airport : IBaseObject, IReportable, IIDUpdateable, IPositionUpdateable
{
    public string Type { get => "AI"; set { } }
    public ulong ID { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public float Longitude { get; set; }
    public float Latitude { get; set; }
    public float AMSL { get; set; }
    public string CountryISO { get; set; }
    public WorldPosition Position
    {
        get
        {
            return WorldPosition.ConstructWorldPosition(Latitude, Longitude);
        }
    }

    public override string ToString()
    {
        return
            $"Airport ID: {ID}, Name: {Name}, Code: {Code}, Longitude: {Longitude}, Latitude: {Latitude}, AMSL: {AMSL}, Country ISO: {CountryISO}";
    }

    public string Accept(INewsProvider visitor) => visitor.VisitAirport(this);
    
    public void UpdateID(ulong newID)
    {
        this.ID = newID;
    }
    
    public void UpdatePosition(float longitude, float latitude, float amsl)
    {
        this.Longitude = longitude;
        this.Latitude = latitude;
        this.AMSL = amsl;
    }
}