using FlightRadar.Interfaces;
namespace FlightRadar.Models;

public class PassengerPlane : IBaseObject
{
    public string Type { get => "PP"; set {} }
    public ulong ID { get; set; }
    public string Serial { get; set; }
    public string CountryISO { get; set; }
    public string Model { get; set; }
    public ushort FirstClassSize { get; set; }
    public ushort BusinessClassSize { get; set; }
    public ushort EconomyClassSize { get; set; }
    public override string ToString()
    {
        return $"PassengerPlane ID: {ID}, Serial: {Serial}, Country ISO: {CountryISO}, Model: {Model}, " +
               $"First Class Size: {FirstClassSize}, Business Class Size: {BusinessClassSize}, " +
               $"Economy Class Size: {EconomyClassSize}";
    }
}