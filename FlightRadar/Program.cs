using FlightRadar.Interfaces;
using FlightRadar.Utilities;

namespace FlightRadar
{ 
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "Data/example_data.ftr";
            
            try
            {
                List<IBaseObject> dataList = new List<IBaseObject>();
                
                Console.WriteLine("Enter minimum delay:");
                int minDelay = int.Parse(Console.ReadLine());
                
                Console.WriteLine("Enter maximum delay:");
                int maxDelay = int.Parse(Console.ReadLine());
                
                NSSDataLoader.RunNSSDataLoader(filePath, minDelay, maxDelay, dataList);
                
                CommandsProcessor commandProcessor = new CommandsProcessor(dataList);
                
                Thread tmp = new Thread(async () =>
                {
                    commandProcessor.ProcessCommands();
                })
                {
                    IsBackground = true
                };
                tmp.Start();
                
                FlightRadarGUIRunner test = new FlightRadarGUIRunner(dataList);
                test.RunInterface();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"File '{filePath}' not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}