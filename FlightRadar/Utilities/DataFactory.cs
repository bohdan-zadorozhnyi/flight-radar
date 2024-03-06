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
        if (values.Length < 8)
            throw new ArgumentException("Invalid number of values for creating Airport object.");

        Airport airport = new Airport();
        airport.ID = ulong.Parse(values[1]);
        airport.Name = values[2];
        airport.Code = values[3];
        airport.Longitude = Parser.FloatParse(values[4]);
        airport.Latitude = Parser.FloatParse(values[5]);
        airport.AMSL = Parser.FloatParse(values[6]);
        airport.CountryISO = values[7];

        return airport;
    }

    private Cargo CreateCargo(string[] values)
    {
        if (values.Length < 5)
            throw new ArgumentException("Invalid number of values for creating Cargo object.");

        Cargo cargo = new Cargo();
        cargo.ID = ulong.Parse(values[1]);
        cargo.Weight = Parser.FloatParse(values[2]);
        cargo.Code = values[3];
        cargo.Description = values[4];

        return cargo;
    }

    private CargoPlane CreateCargoPlane(string[] values)
    {
        if (values.Length < 6)
            throw new ArgumentException("Invalid number of values for creating CargoPlane object.");

        CargoPlane cargoPlane = new CargoPlane();
        cargoPlane.ID = ulong.Parse(values[1]);
        cargoPlane.Serial = values[2];
        cargoPlane.CountryISO = values[3];
        cargoPlane.Model = values[4];
        cargoPlane.MaxLoad = Parser.FloatParse(values[5]);

        return cargoPlane;
    }

    private Crew CreateCrew(string[] values)
    {
        if (values.Length < 8)
            throw new ArgumentException("Invalid number of values for creating Crew object.");

        Crew crew = new Crew();
        crew.ID = ulong.Parse(values[1]);
        crew.Name = values[2];
        crew.Age = ulong.Parse(values[3]);
        crew.Phone = values[4];
        crew.Email = values[5];
        crew.Practice = ushort.Parse(values[6]);
        crew.Role = values[7];

        return crew;
    }

    private Passenger CreatePassenger(string[] values)
    {
        if (values.Length < 8)
            throw new ArgumentException("Invalid number of values for creating Passenger object.");

        Passenger passenger = new Passenger();
        passenger.ID = ulong.Parse(values[1]);
        passenger.Name = values[2];
        passenger.Age = ulong.Parse(values[3]);
        passenger.Phone = values[4];
        passenger.Email = values[5];
        passenger.Class = values[6];
        passenger.Miles = ulong.Parse(values[7]);

        return passenger;
    }

    private PassengerPlane CreatePassengerPlane(string[] values)
    {
        if (values.Length < 8)
            throw new ArgumentException("Invalid number of values for creating PassengerPlane object.");

        PassengerPlane passengerPlane = new PassengerPlane();
        passengerPlane.ID = ulong.Parse(values[1]);
        passengerPlane.Serial = values[2];
        passengerPlane.CountryISO = values[3];
        passengerPlane.Model = values[4];
        passengerPlane.FirstClassSize = ushort.Parse(values[5]);
        passengerPlane.BusinessClassSize = ushort.Parse(values[6]);
        passengerPlane.EconomyClassSize = ushort.Parse(values[7]);

        return passengerPlane;
    }
    
    private Flight CreateFlight(string[] values)
    {
        if (values.Length < 12)
            throw new ArgumentException("Invalid number of values for creating Flight object.");

        Flight flight = new Flight();
        flight.ID = ulong.Parse(values[1]);
        flight.OriginID = ulong.Parse(values[2]);
        flight.TargetID = ulong.Parse(values[3]);
        flight.TakeoffTime = values[4];
        flight.LandingTime = values[5];
        flight.Longitude = Parser.FloatParse(values[6]);
        flight.Latitude = Parser.FloatParse(values[7]);
        flight.AMSL = Parser.FloatParse(values[8]);
        flight.PlaneID = ulong.Parse(values[9]);

        flight.CrewIDs = Parser.UlongListParse(values[10]);
        flight.LoadIDs = Parser.UlongListParse(values[11]);

        return flight;
    }
}