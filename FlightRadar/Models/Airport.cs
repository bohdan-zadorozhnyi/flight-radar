using FlightRadar.Interfaces;
using FlightRadar.Utilities;

namespace FlightRadar.Models
{
    public class Airport : IBaseObject
    {
        public string Type { get => "AI"; set {} }
        public ulong ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public float AMSL { get; set; }
        public string CountryISO { get; set; }

        public override string ToString()
        {
            return $"Airport ID: {ID}, Name: {Name}, Code: {Code}, Longitude: {Longitude}, Latitude: {Latitude}, AMSL: {AMSL}, Country ISO: {CountryISO}";
        }
    }
}