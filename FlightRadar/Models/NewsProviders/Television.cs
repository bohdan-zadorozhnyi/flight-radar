namespace FlightRadar.Models.NewsProviders;
using Interfaces;

public class Television : INewsProvider
{
    public string Name { get; set; }

    public Television(string name)
    {
        Name = name;
    }

    public string VisitAirport(Airport airport) => $"<An image of {airport.Name} airport>";

    public string VisitCargoPlane(CargoPlane cargoPlane) => $"<An image of {cargoPlane.Serial} cargo plane>";

    public string VisitPassengerPlane(PassengerPlane passengerPlane) => $"<An image of {passengerPlane.Serial} passenger plane>";
}
