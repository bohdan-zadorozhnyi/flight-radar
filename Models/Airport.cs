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
        
        public Airport(string[] values)
        {
            if (values.Length < 8)
                throw new ArgumentException("Invalid number of values for creating Airport object.");
            
            ID = ulong.Parse(values[1]);
            Name = values[2];
            Code = values[3];
            Longitude = Parser.FloatParse(values[4]);
            Latitude = Parser.FloatParse(values[5]);
            AMSL = Parser.FloatParse(values[6]);
            CountryISO = values[7];
        }

        public override string ToString()
        {
            return $"Airport ID: {ID}, Name: {Name}, Code: {Code}, Longitude: {Longitude}, Latitude: {Latitude}, AMSL: {AMSL}, Country ISO: {CountryISO}";
        }
    }
}