namespace FlightRadar.Structures;
using Interfaces;

public struct WorldPosition
{
    public float Lat { get; set; }
    public float Long { get; set; }
    
    public WorldPosition(float lat, float lon)
    {
        Lat = lat;
        Long = lon;
    }
    
    public WorldPosition(IPositionUpdateable item)
    {
        Lat = item.Latitude;
        Long = item.Longitude;
    }

    public override string ToString()
    {
        return "{"+$"{Lat}, {Long}"+"}";
    }
    
    public static WorldPosition ConstructWorldPosition(float lat, float lon)
    {
        return new WorldPosition { Lat = lat, Long = lon };
    }
}