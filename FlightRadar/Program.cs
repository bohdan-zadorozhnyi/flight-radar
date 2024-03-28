using FlightRadar.Interfaces;
using System.Globalization;
using FlightRadar.Utilities;

namespace FlightRadar
{ 
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "../../../Data/example_data.ftr.txt";
            
            try
            {
                List<IBaseObject> dataList = new List<IBaseObject>();
                Console.WriteLine("(1) Input from file\n(2) Input from Network Source Simulation");
                int option = int.Parse(Console.ReadLine());
                
                if (option == 1)
                {
                    string data = File.ReadAllText(filePath);
                    var dataLoader = new DataLoader();
                    dataList = dataLoader.LoadData(data);
                }
                else if (option == 2)
                {
                    Console.WriteLine("Enter minimum delay:");
                    int minDelay = int.Parse(Console.ReadLine());
                
                    Console.WriteLine("Enter maximum delay:");
                    int maxDelay = int.Parse(Console.ReadLine());
                
                    NSSDataLoader.RunNSSDataLoader(filePath, minDelay, maxDelay, dataList);
                }
                
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