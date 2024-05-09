namespace FlightRadar.Utilities.Commands;
using Interfaces;

public class PrintCommand : ICommand
{
    private readonly List<IBaseObject> data;
    private readonly JSONSerializer jsonSerializer;
    
    public PrintCommand(List<IBaseObject> data, JSONSerializer serializer)
    {
        this.data = data;
        this.jsonSerializer = serializer;
    }
    
    public void Execute()
    {
        CreateSnapshot();
    }
    
    private void CreateSnapshot()
    {
        try
        {
            string timeStamp = DateTime.Now.ToString("HH_mm_ss");
            string jsonFilePath = $"snapshot_{timeStamp}.json";
            string serializedData = jsonSerializer.Serialize(data);
            File.WriteAllText(jsonFilePath, serializedData);
            Console.WriteLine($"Data serialized successfully. Data saved to {jsonFilePath}.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred while serializing data: {ex.Message}");
        }
    }
}