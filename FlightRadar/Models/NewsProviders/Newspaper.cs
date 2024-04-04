namespace FlightRadar.Models.NewsProviders;
using Interfaces;
using Utilities;

public class Newspaper : INewsProvider
{
    public string Name { get; set; }

    public Newspaper(string name)
    {
        Name = name;
    }

    public string VisitAirport(Airport airport) => $"{Name} - A report from the {airport.Name} airport {Converter.ISOToCountryName(airport.CountryISO)}.";

    public string VisitCargoPlane(CargoPlane cargoPlane) => $"{Name} - An interview with the crew of {cargoPlane.Serial}.";

    public string VisitPassengerPlane(PassengerPlane passengerPlane) => $"{Name} - Breaking news! {passengerPlane.Model} aircraft loses EASA fails certification after inspection of {passengerPlane.Serial}.";
}
