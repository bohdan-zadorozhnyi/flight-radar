using FlightRadar.Interfaces;
namespace FlightRadar.Utilities;

public class CommandsProcessor
{
    private List<IBaseObject> data;
    private readonly JSONSerializer jsonSerializer;

    public CommandsProcessor(List<IBaseObject> _data)
    {
        data = _data;
        jsonSerializer = new JSONSerializer();
    }
    
    public void ProcessCommands()
    {
        while (true)
        {
            Console.WriteLine("Enter a command ('print' or 'exit'): ");
            string command = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(command))
            {
                Console.WriteLine("Invalid command. Please try again.");
                continue;
            }
            
            switch (command.ToLower())
            {
                case "print":
                    CreateSnapshot();
                    break;
                case "exit":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Unknown command. Please try again.");
                    break;
            }
        }
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