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
                string data = File.ReadAllText(filePath);
                
                var dataFactory = new DataFactory();
                var dataLoader = new DataLoader(dataFactory);
                
                var dataList = dataLoader.LoadData(data);
                
                var jsonSerializer = new JSONSerializer();
                string jsonFilePath = "data.json";
                string serializedData = jsonSerializer.Serialize(dataList);
                File.WriteAllText(jsonFilePath, serializedData);
                
                Console.WriteLine("Data serialized and saved to JSON file successfully.");
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