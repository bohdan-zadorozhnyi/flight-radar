using FlightRadar.Interfaces;
namespace FlightRadar.Utilities;

public class DataLoader : IDataLoader
{
    private readonly IDataFactory _dataFactory;

    public DataLoader(IDataFactory dataFactory)
    {
        _dataFactory = dataFactory;
    }

    public List<IBaseObject> LoadData(string data)
    {
        if (string.IsNullOrWhiteSpace(data))
            throw new ArgumentException("Data cannot be empty or null.");

        var dataList = new List<IBaseObject>();

        var lines = data.Split('\n', StringSplitOptions.RemoveEmptyEntries);

        foreach (var line in lines)
        {
            var values = line.Remove(line.Length-1).Split(',', StringSplitOptions.RemoveEmptyEntries);
            var obj = _dataFactory.CreateObject(values);
            if (obj != null)
                dataList.Add(obj);
        }

        return dataList;
    }
}