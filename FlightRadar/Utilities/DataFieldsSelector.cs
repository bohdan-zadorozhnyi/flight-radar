namespace FlightRadar.Utilities;

public class DataFieldsSelector
{
    private static Dictionary<string, List<string>> classFields = new Dictionary<string, List<string>>()
    {
        { "flight", new List<string> { "ID", "Origin", "Target", "TakeoffTime", "LandingTime", "WorldPosition", "AMSL", "Plane" } },
        { "airport", new List<string> { "ID", "Name", "Code", "WorldPosition", "AMSL", "CountryCode" } },
        { "passengerplane", new List<string> { "ID", "Serial", "CountryCode", "Model", "FirstClassSize", "BusinessClassSize", "EconomyClassSize" } },
        { "cargoplane", new List<string> { "ID", "Serial", "CountryCode", "Model", "MaxLoad" } },
        { "cargo", new List<string> { "ID", "Weight", "Code", "Description" } },
        { "passenger", new List<string> { "ID", "Name", "Age", "Phone", "Email", "Class", "Miles" } },
        { "crew", new List<string> { "ID", "Name", "Age", "Phone", "Email", "Practice", "Role" } }
    };
    
    public static List<string> InitializeFields(List<string> fields, string objectClass)
    {
        if (fields.Contains("*"))
        {
            if (classFields.TryGetValue(objectClass.ToLower(), out var allFields))
            {
                return allFields;
            }
            else
            {
                Console.WriteLine("Unknown object class or no fields defined for this class.");
                return null;
            }
        }
        return fields;
    }

}