namespace FlightRadar.Utilities;
using Interfaces;
using Models;

public class Getters
{
    public static void RegisterAirportAccessors(Dictionary<Type, Dictionary<string, Func<IBaseObject, object>>> accessors)
    {
        var airportAccessors = new Dictionary<string, Func<IBaseObject, object>>
        {
            {"ID", item => ((Airport)item).ID},
            {"Name", item => ((Airport)item).Name},
            {"Code", item => ((Airport)item).Code},
            {"WorldPosition", item => ((Airport)item).Position},
            {"AMSL", item => ((Airport)item).AMSL},
            {"CountryCode", item => ((Airport)item).CountryISO}
        };
        accessors.Add(typeof(Airport), airportAccessors);
    }

    public static void RegisterFlightAccessors(Dictionary<Type, Dictionary<string, Func<IBaseObject, object>>> accessors)
    {
        var flightAccessors = new Dictionary<string, Func<IBaseObject, object>>
        {
            {"ID", item => ((Flight)item).ID},
            {"OriginID", item => ((Flight)item).OriginID},
            {"TargetID", item => ((Flight)item).TargetID},
            {"TakeoffTime", item => ((Flight)item).TakeoffTime},
            {"LandingTime", item => ((Flight)item).LandingTime},
            {"WorldPosition", item => ((Flight)item).Position},
            {"AMSL", item => ((Flight)item).AMSL},
            {"PlaneID", item => ((Flight)item).PlaneID}
        };
        accessors.Add(typeof(Flight), flightAccessors);
    }

    public static void RegisterCargoAccessors(Dictionary<Type, Dictionary<string, Func<IBaseObject, object>>> accessors)
    {
        var cargoAccessors = new Dictionary<string, Func<IBaseObject, object>>
        {
            {"ID", item => ((Cargo)item).ID},
            {"Weight", item => ((Cargo)item).Weight},
            {"Code", item => ((Cargo)item).Code},
            {"Description", item => ((Cargo)item).Description}
        };
        accessors.Add(typeof(Cargo), cargoAccessors);
    }

    public static void RegisterCargoPlaneAccessors(Dictionary<Type, Dictionary<string, Func<IBaseObject, object>>> accessors)
    {
        var cargoplaneAccessors = new Dictionary<string, Func<IBaseObject, object>>
        {
            {"ID", item => ((CargoPlane)item).ID},
            {"Serial", item => ((CargoPlane)item).Serial},
            {"CountryCode", item => ((CargoPlane)item).CountryISO},
            {"Model", item => ((CargoPlane)item).Model},
            {"MaxLoad", item => ((CargoPlane)item).MaxLoad}
        };
        accessors.Add(typeof(CargoPlane), cargoplaneAccessors);
    }

    public static void RegisterCrewAccessors(Dictionary<Type, Dictionary<string, Func<IBaseObject, object>>> accessors)
    {
        var crewAccessors = new Dictionary<string, Func<IBaseObject, object>>
        {
            {"ID", item => ((Crew)item).ID},
            {"Name", item => ((Crew)item).Name},
            {"Age", item => ((Crew)item).Age},
            {"Phone", item => ((Crew)item).Phone},
            {"Email", item => ((Crew)item).Email},
            {"Practice", item => ((Crew)item).Practice},
            {"Role", item => ((Crew)item).Role}
        };
        accessors.Add(typeof(Crew), crewAccessors);
    }

    public static void RegisterPassengerAccessors(Dictionary<Type, Dictionary<string, Func<IBaseObject, object>>> accessors)
    {
        var passengerAccessors = new Dictionary<string, Func<IBaseObject, object>>
        {
            {"ID", item => ((Passenger)item).ID},
            {"Name", item => ((Passenger)item).Name},
            {"Age", item => ((Passenger)item).Age},
            {"Phone", item => ((Passenger)item).Phone},
            {"Email", item => ((Passenger)item).Email},
            {"Class", item => ((Passenger)item).Class},
            {"Miles", item => ((Passenger)item).Miles}
        };
        accessors.Add(typeof(Passenger), passengerAccessors);
    }

    public static void RegisterPassengerPlaneAccessors(Dictionary<Type, Dictionary<string, Func<IBaseObject, object>>> accessors)
    {
        var passengerplaneAccessors = new Dictionary<string, Func<IBaseObject, object>>
        {
            {"ID", item => ((PassengerPlane)item).ID},
            {"Serial", item => ((PassengerPlane)item).Serial},
            {"CountryCode", item => ((PassengerPlane)item).CountryISO},
            {"Model", item => ((PassengerPlane)item).Model},
            {"FirstClassSize", item => ((PassengerPlane)item).FirstClassSize},
            {"BusinessClassSize", item => ((PassengerPlane)item).BusinessClassSize},
            {"EconomyClassSize", item => ((PassengerPlane)item).EconomyClassSize}
        };
        accessors.Add(typeof(PassengerPlane), passengerplaneAccessors);
    }
}