using FlightRadar.Models;
using FlightRadar.Interfaces;
using NetworkSourceSimulator;
using System.Text;
using System.Globalization;
namespace FlightRadar.Utilities.Factories;

public class MessageDataFactory
{
    private readonly Dictionary<string, Func<Message, IBaseObject>> factoryMethodsMessage;

    public MessageDataFactory()
    {
        factoryMethodsMessage = new Dictionary<string, Func<Message, IBaseObject>>
        {
            { "NAI", CreateAirport },
            { "NCA", CreateCargo },
            { "NCP", CreateCargoPlane },
            { "NCR", CreateCrew },
            { "NPA", CreatePassenger },
            { "NPP", CreatePassengerPlane },
            { "NFL", CreateFlight }
        };
    }
    
    public IBaseObject CreateObject(Message message)
    {
        if (message.MessageBytes.Length <= 0)
            throw new ArgumentException("Invalid data for creating object.");
    
        string type = Encoding.ASCII.GetString(message.MessageBytes, 0, 3);
    
        if (factoryMethodsMessage.ContainsKey(type))
            return factoryMethodsMessage[type](message);
    
        return null;
    }
    
    private Airport CreateAirport(Message message)
    {
        byte[] bytes = message.MessageBytes;
        if (bytes.Length < 35)
            throw new ArgumentException("Invalid number of bytes for creating Airport object.");

        Airport airport = new Airport();
        airport.ID = BitConverter.ToUInt64(bytes, 7);
        ushort nameLength = BitConverter.ToUInt16(bytes, 15);
        airport.Name = Encoding.ASCII.GetString(bytes, 17, nameLength);
        airport.Code = Encoding.ASCII.GetString(bytes, 17 + nameLength, 3);
        airport.Longitude = BitConverter.ToSingle(bytes, 20 + nameLength);
        airport.Latitude = BitConverter.ToSingle(bytes, 24 + nameLength);
        airport.AMSL = BitConverter.ToSingle(bytes, 28 + nameLength);
        airport.CountryISO = Encoding.ASCII.GetString(bytes, 32 + nameLength, 3);

        return airport;
    }

    private static Cargo CreateCargo(Message message)
    {
        byte[] bytes = message.MessageBytes;
        if (bytes.Length < 27)
            throw new ArgumentException("Invalid number of bytes for creating Cargo object.");

        Cargo cargo = new Cargo();
        cargo.ID = BitConverter.ToUInt64(bytes, 7);
        cargo.Weight = BitConverter.ToSingle(bytes, 15);
        cargo.Code = Encoding.ASCII.GetString(bytes, 19, 6);
        ushort descriptionLength = BitConverter.ToUInt16(bytes, 25);
        cargo.Description = Encoding.ASCII.GetString(bytes, 27, descriptionLength);

        return cargo;
    }

    private CargoPlane CreateCargoPlane(Message message)
    {
        byte[] bytes = message.MessageBytes;
        if (bytes.Length < 30)
            throw new ArgumentException("Invalid number of bytes for creating CargoPlane object.");

        CargoPlane cargoPlane = new CargoPlane();
        cargoPlane.ID = BitConverter.ToUInt64(bytes, 7);
        cargoPlane.Serial = Encoding.ASCII.GetString(bytes, 15, 10).TrimEnd('\0');
        cargoPlane.CountryISO = Encoding.ASCII.GetString(bytes, 25, 3);
        ushort modelLength = BitConverter.ToUInt16(bytes, 28);
        cargoPlane.Model = Encoding.ASCII.GetString(bytes, 30, modelLength);
        cargoPlane.MaxLoad = BitConverter.ToSingle(bytes, 30 + modelLength);

        return cargoPlane;
    }

    private Crew CreateCrew(Message message)
    {
        byte[] bytes = message.MessageBytes;
        if (bytes.Length < 36)
            throw new ArgumentException("Invalid number of bytes for creating Crew object.");
        
        Crew crew = new Crew();
        crew.ID = BitConverter.ToUInt64(bytes, 7);
        ushort nameLength = BitConverter.ToUInt16(bytes, 15);
        crew.Name = Encoding.ASCII.GetString(bytes, 17, nameLength);
        crew.Age = BitConverter.ToUInt16(bytes, 17 + nameLength);
        crew.Phone = Encoding.ASCII.GetString(bytes, 19 + nameLength, 12);
        ushort emailLength = BitConverter.ToUInt16(bytes, 31 + nameLength);
        crew.Email = Encoding.ASCII.GetString(bytes, 33 + nameLength, emailLength);
        crew.Practice = BitConverter.ToUInt16(bytes, 33 + nameLength + emailLength);
        crew.Role = Encoding.ASCII.GetString(bytes, 35 + nameLength + emailLength, 1);
        
        return crew;
    }

    private Passenger CreatePassenger(Message message)
    {
        byte[] bytes = message.MessageBytes;
        if (bytes.Length < 42)
            throw new ArgumentException("Invalid number of bytes for creating Passenger object.");

        Passenger passenger = new Passenger();
        passenger.ID = BitConverter.ToUInt64(bytes, 7);
        ushort nameLength = BitConverter.ToUInt16(bytes, 15);
        passenger.Name = Encoding.ASCII.GetString(bytes, 17, nameLength);
        passenger.Age = BitConverter.ToUInt16(bytes, 17 + nameLength);
        passenger.Phone = Encoding.ASCII.GetString(bytes, 19 + nameLength, 12);
        ushort emailLength = BitConverter.ToUInt16(bytes, 31 + nameLength);
        passenger.Email = Encoding.ASCII.GetString(bytes, 33 + nameLength, emailLength);
        passenger.Class = Encoding.ASCII.GetString(bytes, 33 + nameLength + emailLength, 1);
        passenger.Miles = BitConverter.ToUInt64(bytes, 34 + nameLength + emailLength);
        
        return passenger;
    }

    private PassengerPlane CreatePassengerPlane(Message message)
    {
        byte[] bytes = message.MessageBytes;
        if (bytes.Length < 36)
            throw new ArgumentException("Invalid number of bytes for creating PassengerPlane object.");

        PassengerPlane passengerPlane = new PassengerPlane();
        passengerPlane.ID = BitConverter.ToUInt64(bytes, 7);
        passengerPlane.Serial = Encoding.ASCII.GetString(bytes, 15, 10).TrimEnd('\0');
        passengerPlane.CountryISO = Encoding.ASCII.GetString(bytes, 25, 3);
        ushort modelLength = BitConverter.ToUInt16(bytes, 28);
        passengerPlane.Model = Encoding.ASCII.GetString(bytes, 30, modelLength);
        passengerPlane.FirstClassSize = BitConverter.ToUInt16(bytes, 30 + modelLength);
        passengerPlane.BusinessClassSize = BitConverter.ToUInt16(bytes, 32 + modelLength);
        passengerPlane.EconomyClassSize = BitConverter.ToUInt16(bytes, 34 + modelLength);

        return passengerPlane;
    }
  
    private Flight CreateFlight(Message message)
    {
        byte[] bytes = message.MessageBytes;
        if (bytes.Length < 59)
            throw new ArgumentException("Invalid number of bytes for creating Flight object.");

        Flight flight = new Flight();
        flight.ID = BitConverter.ToUInt64(bytes, 7);
        flight.OriginID = BitConverter.ToUInt64(bytes, 15);
        flight.TargetID = BitConverter.ToUInt64(bytes, 23);
        
        DateTime takeoffTime = DateTimeOffset.FromUnixTimeMilliseconds(BitConverter.ToInt64(bytes, 31)).UtcDateTime;
        DateTime landingTime = DateTimeOffset.FromUnixTimeMilliseconds(BitConverter.ToInt64(bytes, 39)).UtcDateTime;

        flight.TakeoffTime = takeoffTime;
        flight.LandingTime = landingTime;
        
        flight.PlaneID = BitConverter.ToUInt64(bytes, 47);

        ushort crewCount = BitConverter.ToUInt16(bytes, 55);
        int crewStartIndex = 57;
        flight.CrewIDs = new List<ulong>();
        for (int i = 0; i < crewCount; i++)
        {
            flight.CrewIDs.Add(BitConverter.ToUInt64(bytes, crewStartIndex + i * 8));
        }

        ushort loadCount = BitConverter.ToUInt16(bytes, 57 + crewCount * 8);
        int loadStartIndex = 59 + crewCount * 8;
        flight.LoadIDs = new List<ulong>();
        for (int i = 0; i < loadCount; i++)
        {
            flight.LoadIDs.Add(BitConverter.ToUInt64(bytes, loadStartIndex + i * 8));
        }

        return flight;
    }
}