using FlightRadar.Interfaces;
using FlightRadar.Utilities;
namespace FlightRadar.Models;

public class Flight : IBaseObject
{
    public string Type { get => "FL"; set {} }
    public ulong ID { get; set; }
    public ulong OriginID { get; set; }
    public ulong TargetID { get; set; }
    public DateTime TakeoffTime { get; set; }
    public DateTime LandingTime { get; set; }
    public float Longitude { get; set; }
    public float Latitude { get; set; }
    public float AMSL { get; set; }
    public ulong PlaneID { get; set; }
    public List<ulong> CrewIDs { get; set; }
    public List<ulong> LoadIDs { get; set; }

    public override string ToString()
    {
        var crewIDsString = string.Join(", ", CrewIDs);
        var loadIDsString = string.Join(", ", LoadIDs);

        return $"Flight ID: {ID}, Origin ID: {OriginID}, Target ID: {TargetID}, Takeoff Time: {TakeoffTime}, " +
               $"Landing Time: {LandingTime}, Longitude: {Longitude}, Latitude: {Latitude}, AMSL: {AMSL}, " +
               $"Plane ID: {PlaneID}, Crew IDs: {crewIDsString}, Load IDs: {loadIDsString}";
    }
}