using FlightRadar.Interfaces;
using FlightRadar.Utilities;
namespace FlightRadar.Models;

public class Flight : IBaseObject
{
    public string Type { get => "FL"; set {} }
    public ulong ID { get; set; }
    public ulong OriginID { get; set; }
    public ulong TargetID { get; set; }
    public string TakeoffTime { get; set; }
    public string LandingTime { get; set; }
    public float Longitude { get; set; }
    public float Latitude { get; set; }
    public float AMSL { get; set; }
    public ulong PlaneID { get; set; }
    public ulong[] CrewIDs { get; set; }
    public ulong[] LoadIDs { get; set; }
    
    public Flight(string[] values)
    {
        if (values.Length < 12)
            throw new ArgumentException("Invalid number of values for creating Flight object.");

        ID = ulong.Parse(values[1]);
        OriginID = ulong.Parse(values[2]);
        TargetID = ulong.Parse(values[3]);
        TakeoffTime = values[4];
        LandingTime = values[5];
        Longitude = Parser.FloatParse(values[6]);
        Latitude = Parser.FloatParse(values[7]);
        AMSL = Parser.FloatParse(values[8]);
        PlaneID = ulong.Parse(values[9]);

        CrewIDs = Parser.UlongArrayParse(values[10]);
        LoadIDs = Parser.UlongArrayParse(values[11]);
    }

    public override string ToString()
    {
        var crewIDsString = string.Join(", ", CrewIDs);
        var loadIDsString = string.Join(", ", LoadIDs);

        return $"Flight ID: {ID}, Origin ID: {OriginID}, Target ID: {TargetID}, Takeoff Time: {TakeoffTime}, " +
               $"Landing Time: {LandingTime}, Longitude: {Longitude}, Latitude: {Latitude}, AMSL: {AMSL}, " +
               $"Plane ID: {PlaneID}, Crew IDs: {crewIDsString}, Load IDs: {loadIDsString}";
    }
}