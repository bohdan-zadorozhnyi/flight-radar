using FlightRadar.Interfaces;
using FlightRadar.Utilities;

namespace FlightRadar
{ 
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string filePath = "../../../Data/example_data.ftr.txt";

                Console.WriteLine("Enter minimum delay:");
                int minDelay = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter maximum delay:");
                int maxDelay = int.Parse(Console.ReadLine());

                List<IBaseObject> data = new List<IBaseObject>();

                NSSDataLoader.RunNSSDataLoader(filePath, minDelay, maxDelay, data);
                
                CommandsProcessor commandProcessor = new CommandsProcessor(data);
                commandProcessor.ProcessCommands();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
            }
        }
    }
}