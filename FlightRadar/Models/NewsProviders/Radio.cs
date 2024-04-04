namespace FlightRadar.Models.NewsProviders;
using Interfaces;

public class Radio : INewsProvider
{
    public string Name { get; set; }

    public Radio(string name)
    {
        Name = name;
    }

    public string VisitAirport(Airport airport) => $"Reporting for {Name}, Ladies and Gentlemen, we are at the {airport.Name} airport.";

    public string VisitCargoPlane(CargoPlane cargoPlane) => $"Reporting for {Name}, Ladies and Gentlemen, we are seeing the {cargoPlane.Serial} aircraft fly above us.";

    public string VisitPassengerPlane(PassengerPlane passengerPlane) => $"Reporting for {Name}, Ladies and Gentlemen, we’ve just witnessed {passengerPlane.Serial} take off.";
}
