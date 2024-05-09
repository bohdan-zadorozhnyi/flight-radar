namespace FlightRadar.Utilities.Factories;
using Models;
using Interfaces;
using System.Globalization;

public class QueryDataFactory
{
    private readonly Dictionary<string, Func<Dictionary<string, string>, IBaseObject>> factoryMethodsArray;

    public QueryDataFactory()
    {
        factoryMethodsArray = new Dictionary<string, Func<Dictionary<string, string>, IBaseObject>>
        {
            { "Airport", CreateAirport },
            { "Cargo", CreateCargo },
            { "CargoPlane", CreateCargoPlane },
            { "Crew", CreateCrew },
            { "Passenger", CreatePassenger },
            { "PassengerPlane", CreatePassengerPlane },
            { "Flight", CreateFlight }
        };
    }
    
    public IBaseObject CreateObject(string objectClass, Dictionary<string, string> properties)
    {
        if (factoryMethodsArray.ContainsKey(objectClass))
            return factoryMethodsArray[objectClass](properties);
    
        return null;
    }
    
    private static Airport CreateAirport(Dictionary<string, string> properties)
    {
        Airport airport = new Airport();
        airport.ID = properties.TryGetValue("ID", out string idValue) && ulong.TryParse(idValue, out ulong id) ? id : 0;
        airport.Name = properties.TryGetValue("Name", out string name) ? name : "Unknown";
        airport.Code = properties.TryGetValue("Code", out string code) ? code : "N/A";
        airport.Longitude = properties.TryGetValue("WorldPosition.Long", out string longitude) ? Parser.FloatParse(longitude) : 0f;
        airport.Latitude = properties.TryGetValue("WorldPosition.Lat", out string latitude) ? Parser.FloatParse(latitude) : 0f;
        airport.AMSL = properties.TryGetValue("AMSL", out string amsl) ? Parser.FloatParse(amsl) : 0f;
        airport.CountryISO = properties.TryGetValue("CountryCode", out string countryISO) ? countryISO : "Undefined";

        return airport;
    }

    private static Cargo CreateCargo(Dictionary<string, string> properties)
    {
        Cargo cargo = new Cargo();
        cargo.ID = properties.TryGetValue("ID", out string idValue) && ulong.TryParse(idValue, out ulong id) ? id : 0;
        cargo.Weight = properties.TryGetValue("Weight", out string weightValue) ? Parser.FloatParse(weightValue) : 0f;
        cargo.Code = properties.TryGetValue("Code", out string code) ? code : "Unknown";
        cargo.Description = properties.TryGetValue("Description", out string description) ? description : "No description available";

        return cargo;
    }

    private CargoPlane CreateCargoPlane(Dictionary<string, string> properties)
    {
        CargoPlane cargoPlane = new CargoPlane();
        cargoPlane.ID = properties.TryGetValue("ID", out string idValue) && ulong.TryParse(idValue, out ulong id) ? id : 0;
        cargoPlane.Serial = properties.TryGetValue("Serial", out string serial) ? serial : "Unknown Serial";
        cargoPlane.CountryISO = properties.TryGetValue("CountryCode", out string countryISO) ? countryISO : "Unknown Country";
        cargoPlane.Model = properties.TryGetValue("Model", out string model) ? model : "Unknown Model";
        cargoPlane.MaxLoad = properties.TryGetValue("MaxLoad", out string maxLoadValue) ? Parser.FloatParse(maxLoadValue) : 0f;

        return cargoPlane;
    }
    
    private Crew CreateCrew(Dictionary<string, string> properties)
    {
        Crew crew = new Crew();
        crew.ID = properties.TryGetValue("ID", out string idValue) && ulong.TryParse(idValue, out ulong id) ? id : 0;
        crew.Name = properties.TryGetValue("Name", out string name) ? name : "Unknown";
        crew.Age = properties.TryGetValue("Age", out string ageValue) && ulong.TryParse(ageValue, out ulong age) ? age : 0;
        crew.Phone = properties.TryGetValue("Phone", out string phone) ? phone : "No Phone";
        crew.Email = properties.TryGetValue("Email", out string email) ? email : "No Email";
        crew.Practice = properties.TryGetValue("Practice", out string practiceValue) && ushort.TryParse(practiceValue, out ushort practice) ? practice : (ushort)0;
        crew.Role = properties.TryGetValue("Role", out string role) ? role : "Unknown Role";

        return crew;
    }

    private Passenger CreatePassenger(Dictionary<string, string> properties)
    {
        Passenger passenger = new Passenger();
        passenger.ID = properties.TryGetValue("ID", out string idValue) && ulong.TryParse(idValue, out ulong id) ? id : 0;
        passenger.Name = properties.TryGetValue("Name", out string name) ? name : "Unknown";
        passenger.Age = properties.TryGetValue("Age", out string ageValue) && ulong.TryParse(ageValue, out ulong age) ? age : 0;
        passenger.Phone = properties.TryGetValue("Phone", out string phone) ? phone : "No Phone";
        passenger.Email = properties.TryGetValue("Email", out string email) ? email : "No Email";
        passenger.Class = properties.TryGetValue("Class", out string passengerClass) ? passengerClass : "Economy";
        passenger.Miles = properties.TryGetValue("Miles", out string milesValue) && ulong.TryParse(milesValue, out ulong miles) ? miles : 0;

        return passenger;
    }
    
    private PassengerPlane CreatePassengerPlane(Dictionary<string, string> properties)
    {
        PassengerPlane passengerPlane = new PassengerPlane();
        passengerPlane.ID = properties.TryGetValue("ID", out string idValue) && ulong.TryParse(idValue, out ulong id) ? id : 0;
        passengerPlane.Serial = properties.TryGetValue("Serial", out string serial) ? serial : "Unknown Serial";
        passengerPlane.CountryISO = properties.TryGetValue("CountryCode", out string countryISO) ? countryISO : "Unknown Country";
        passengerPlane.Model = properties.TryGetValue("Model", out string model) ? model : "Standard Model";
        passengerPlane.FirstClassSize = properties.TryGetValue("FirstClassSize", out string firstClassSize) && ushort.TryParse(firstClassSize, out ushort firstClass) ? firstClass : (ushort)0;
        passengerPlane.BusinessClassSize = properties.TryGetValue("BusinessClassSize", out string businessClassSize) && ushort.TryParse(businessClassSize, out ushort businessClass) ? businessClass : (ushort)0;
        passengerPlane.EconomyClassSize = properties.TryGetValue("EconomyClassSize", out string economyClassSize) && ushort.TryParse(economyClassSize, out ushort economyClass) ? economyClass : (ushort)0;

        return passengerPlane;
    }
    
    private Flight CreateFlight(Dictionary<string, string> properties)
    {
        Flight flight = new Flight();
        flight.ID = properties.TryGetValue("ID", out string idValue) && ulong.TryParse(idValue, out ulong id) ? id : 0;
        flight.OriginID = properties.TryGetValue("OriginID", out string originIdValue) && ulong.TryParse(originIdValue, out ulong originId) ? originId : 0;
        flight.TargetID = properties.TryGetValue("TargetID", out string targetIdValue) && ulong.TryParse(targetIdValue, out ulong targetId) ? targetId : 0;
        flight.TakeoffTime =
            properties.TryGetValue("TakeoffTime", out string takeoffTimeValue) && DateTime.TryParseExact(
                takeoffTimeValue, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime takeoffTime)
                ? takeoffTime
                : DateTime.Now;
        flight.LandingTime =
            properties.TryGetValue("LandingTime", out string landingTimeValue) && DateTime.TryParseExact(
                landingTimeValue, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime landingTime)
                ? landingTime
                : DateTime.Now.AddHours(2);
        flight.Longitude = properties.TryGetValue("WorldPosition.Long", out string longitudeValue) ? Parser.FloatParse(longitudeValue) : 0f;
        flight.Latitude = properties.TryGetValue("WorldPosition.Lat", out string latitudeValue) ? Parser.FloatParse(latitudeValue) : 0f;
        flight.AMSL = properties.TryGetValue("AMSL", out string amslValue) ? Parser.FloatParse(amslValue) : 0f;
        flight.PlaneID = properties.TryGetValue("PlaneID", out string planeIdValue) && ulong.TryParse(planeIdValue, out ulong planeId) ? planeId : 0;
        flight.CrewIDs = new List<ulong>();
        flight.LoadIDs = new List<ulong>();
        return flight;
    }
}