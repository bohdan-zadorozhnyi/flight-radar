namespace FlightRadar.Interfaces;
using Models;

public interface INewsProvider
{
    string VisitAirport(Airport airport);
    string VisitCargoPlane(CargoPlane cargoPlane);
    string VisitPassengerPlane(PassengerPlane passengerPlane);
}