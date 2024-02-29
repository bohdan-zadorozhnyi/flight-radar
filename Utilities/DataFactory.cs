using FlightRadar.Models;
using FlightRadar.Interfaces;
namespace FlightRadar.Utilities;

public class DataFactory : IDataFactory
{
    private readonly Dictionary<string, Func<string[], IBaseObject>> factoryMethods;

    public DataFactory()
    {
        factoryMethods = new Dictionary<string, Func<string[], IBaseObject>>
        {
            { "AI", CreateAirport },
            { "CA", CreateCargo },
            { "CP", CreateCargoPlane },
            { "C", CreateCrew },
            { "P", CreatePassenger },
            { "PP", CreatePassengerPlane },
            { "FL", CreateFlight }
        };
    }

    public IBaseObject CreateObject(string[] values)
    {
        if (values == null || values.Length < 2)
            throw new ArgumentException("Invalid values for creating object.");

        string type = values[0];

        if (factoryMethods.ContainsKey(type))
            return factoryMethods[type](values);

        return null;
    }
    
    private Airport CreateAirport(string[] values)
    {
        return new Airport(values);
    }

    private Cargo CreateCargo(string[] values)
    {
        return new Cargo(values);
    }

    private CargoPlane CreateCargoPlane(string[] values)
    {
        return new CargoPlane(values);
    }

    private Crew CreateCrew(string[] values)
    {
        return new Crew(values);
    }

    private Passenger CreatePassenger(string[] values)
    {
        return new Passenger(values);
    }

    private PassengerPlane CreatePassengerPlane(string[] values)
    {
        return new PassengerPlane(values);
    }
    
    private Flight CreateFlight(string[] values)
    {
        return new Flight(values);
    }
}