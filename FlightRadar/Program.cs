using FlightRadar.Interfaces;
using System.Globalization;
using FlightRadar.Utilities;

namespace FlightRadar
{ 
    class Program
    {
        static void Main(string[] args)
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo("en-US");
            string filePath = "../../../Data/example_data.ftr.txt";
            
            try
            {
                string data = File.ReadAllText(filePath);
                var dataLoader = new DataLoader();
                var dataList = dataLoader.LoadData(data);
                
                var jsonSerializer = new JSONSerializer();
                string jsonFilePath = "data.json";
                string serializedData = jsonSerializer.Serialize(dataList);
                File.WriteAllText(jsonFilePath, serializedData);
                
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