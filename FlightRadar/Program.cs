using FlightRadar.Interfaces;
using FlightRadar.Utilities;

namespace FlightRadar
{ 
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "Data/example_data.ftr";
            string updatesFilePath = "Data/example.ftre";
            
            try
            {
                List<IBaseObject> dataList = new List<IBaseObject>();
                
                string data = File.ReadAllText(filePath);
                var dataLoader = new DataLoader();
                dataList = dataLoader.LoadData(data);
                
                
                Console.WriteLine("Enter minimum delay:");
                int minDelay = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter maximum delay:");
                int maxDelay = int.Parse(Console.ReadLine());
                
                NSSDataLoader.RunNSSDataLoader(updatesFilePath, minDelay, maxDelay, dataList);
                
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