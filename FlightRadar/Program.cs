using FlightRadar.Interfaces;
using FlightRadar.Utilities;
using FlightRadar.Utilities.Commands;

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