namespace FlightRadar.Interfaces;

public interface IPositionUpdateable
{
    public float Longitude { get; set; }
    public float Latitude { get; set; }
    public float AMSL { get; set; }
    void UpdatePosition(float longitude, float latitude, float amsl);
}