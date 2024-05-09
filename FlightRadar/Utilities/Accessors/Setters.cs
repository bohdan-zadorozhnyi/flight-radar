namespace FlightRadar.Utilities;
using Interfaces;
using Models;
using System.Globalization;

public class Setters
{
    public static void RegisterAirportSetters(Dictionary<Type, Dictionary<string, Action<IBaseObject, Func<object>>>> setters)
    {
        var airportSetters = new Dictionary<string, Action<IBaseObject, Func<object>>>
        {
            {"ID", (obj, value) => ((Airport)obj).ID = (ulong) value() },
            {"Name", (obj, value) => ((Airport)obj).Name = value().ToString() },
            {"Code", (obj, value) => ((Airport)obj).Code = value().ToString() },
            {"WorldPosition.Lat", (obj, value) => ((Airport)obj).Latitude = Parser.FloatParse(value().ToString()) },
            {"WorldPosition.Long", (obj, value) => ((Airport)obj).Longitude = Parser.FloatParse(value().ToString()) },
            {"AMSL", (obj, value) => ((Airport)obj).AMSL = Parser.FloatParse(value().ToString()) },
            {"CountryCode", (obj, value) => ((Airport)obj).CountryISO = value().ToString() }
        };
        setters.Add(typeof(Airport), airportSetters);
    }
    
    public static void RegisterFlightSetters(Dictionary<Type, Dictionary<string, Action<IBaseObject, Func<object>>>> setters)
    {
        var flightSetters = new Dictionary<string, Action<IBaseObject, Func<object>>>
        {
            {"ID", (obj, value) => ((Flight)obj).ID = (ulong) value()},
            {"OriginID", (obj, value)  => ((Flight)obj).OriginID = (ulong) value()},
            {"TargetID", (obj, value)  => ((Flight)obj).TargetID = (ulong) value()},
            {"TakeoffTime", (obj, value)  => ((Flight)obj).TakeoffTime = DateTime.TryParseExact(
                value().ToString(), "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime takeoffTime)
                ? takeoffTime
                : DateTime.Now},
            {"LandingTime", (obj, value)  => ((Flight)obj).LandingTime = DateTime.TryParseExact(
                value().ToString(), "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime takeoffTime)
                ? takeoffTime
                : DateTime.Now},
            {"WorldPosition.Lat", (obj, value)  => ((Flight)obj).Latitude  = Parser.FloatParse(value().ToString()) },
            {"WorldPosition.Long", (obj, value)  => ((Flight)obj).Longitude  = Parser.FloatParse(value().ToString()) },
            {"AMSL", (obj, value)  => ((Flight)obj).AMSL = Parser.FloatParse(value().ToString()) },
            {"PlaneID", (obj, value)  => ((Flight)obj).PlaneID  = (ulong) value()},
        };
        setters.Add(typeof(Flight), flightSetters);
    }
    
    public static void RegisterCargoSetters(Dictionary<Type, Dictionary<string, Action<IBaseObject, Func<object>>>> setters)
    {
        var cargoSetters = new Dictionary<string, Action<IBaseObject, Func<object>>>
        {
            {"ID", (obj, value) => ((Cargo)obj).ID = (ulong) value()},
            {"Weight", (obj, value) => ((Cargo)obj).Weight = Parser.FloatParse(value().ToString())},
            {"Code", (obj, value) => ((Cargo)obj).Code = value().ToString()},
            {"Description", (obj, value) => ((Cargo)obj).Description = value().ToString()}
        };
        setters.Add(typeof(Cargo), cargoSetters);
    }
    
    public static void RegisterCargoPlaneSetters(Dictionary<Type, Dictionary<string, Action<IBaseObject, Func<object>>>> setters)
    {
        var cargoplaneSetters = new Dictionary<string, Action<IBaseObject, Func<object>>>
        {
            {"ID", (obj, value) => ((CargoPlane)obj).ID = (ulong) value()},
            {"Serial", (obj, value) => ((CargoPlane)obj).Serial = value().ToString()},
            {"CountryCode", (obj, value) => ((CargoPlane)obj).CountryISO = value().ToString()},
            {"Model", (obj, value) => ((CargoPlane)obj).Model = value().ToString()},
            {"MaxLoad", (obj, value) => ((CargoPlane)obj).MaxLoad = Parser.FloatParse(value().ToString())}
        };
        setters.Add(typeof(CargoPlane), cargoplaneSetters);
    }
    
    public static void RegisterCrewSetters(Dictionary<Type, Dictionary<string, Action<IBaseObject, Func<object>>>> setters)
    {
        var crewSetters = new Dictionary<string, Action<IBaseObject, Func<object>>>
        {
            {"ID", (obj, value) => ((Crew)obj).ID = (ulong) value()},
            {"Name", (obj, value) => ((Crew)obj).Name = value().ToString()},
            {"Age", (obj, value) => ((Crew)obj).Age = (ulong) value()},
            {"Phone", (obj, value) => ((Crew)obj).Phone = value().ToString()},
            {"Email", (obj, value) => ((Crew)obj).Email = value().ToString()},
            {"Practice", (obj, value) => ((Crew)obj).Practice = (ushort) value()},
            {"Role", (obj, value) => ((Crew)obj).Role = value().ToString()}
        };
        setters.Add(typeof(Crew), crewSetters);
    }
    
    public static void RegisterPassengerSetters(Dictionary<Type, Dictionary<string, Action<IBaseObject, Func<object>>>> setters)
    {
        var passengerSetters = new Dictionary<string, Action<IBaseObject, Func<object>>>
        {
            {"ID", (obj, value) => ((Passenger)obj).ID = (ulong) value()},
            {"Name", (obj, value) => ((Passenger)obj).Name = value().ToString()},
            {"Age", (obj, value) => ((Passenger)obj).Age = (ulong) value()},
            {"Phone", (obj, value) => ((Passenger)obj).Phone = value().ToString()},
            {"Email", (obj, value) => ((Passenger)obj).Email = value().ToString()},
            {"Class", (obj, value) => ((Passenger)obj).Class = value().ToString()},
            {"Miles", (obj, value) => ((Passenger)obj).Miles = (ulong) value()}
        };
        setters.Add(typeof(Passenger), passengerSetters);
    }
    
    public static void RegisterPassengerPlaneSetters(Dictionary<Type, Dictionary<string, Action<IBaseObject, Func<object>>>> setters)
    {
        var passengerplaneSetters = new Dictionary<string, Action<IBaseObject, Func<object>>>
        {
            {"ID", (obj, value) => ((PassengerPlane)obj).ID = (ulong) value()},
            {"Serial", (obj, value) => ((PassengerPlane)obj).Serial = value().ToString()},
            {"CountryCode", (obj, value) => ((PassengerPlane)obj).CountryISO = value().ToString()},
            {"Model", (obj, value) => ((PassengerPlane)obj).Model = value().ToString()},
            {"FirstClassSize", (obj, value) => ((PassengerPlane)obj).FirstClassSize = (ushort) value()},
            {"BusinessClassSize", (obj, value) => ((PassengerPlane)obj).BusinessClassSize = (ushort) value()},
            {"EconomyClassSize", (obj, value) => ((PassengerPlane)obj).EconomyClassSize = (ushort) value()}
        };
        setters.Add(typeof(PassengerPlane), passengerplaneSetters);
    }
}