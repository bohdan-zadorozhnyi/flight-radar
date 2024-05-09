namespace FlightRadar.Utilities.Commands;
using Interfaces;

public class DisplayCommand: ICommand
{
    private readonly List<IBaseObject> _data;
    public string _ObjectClass { get; private set; }
    public List<string> _Fields { get; private set; }
    public string _Conditions { get; private set; }
    private readonly PropertyAccessors _propertyAccessors = new PropertyAccessors();


    public DisplayCommand(List<IBaseObject> data, string ObjectClass, List<string> Fields, string Conditions)
    {
        this._data = data;
        this._ObjectClass = ObjectClass;
        this._Fields = Fields;
        this._Conditions = Conditions;
    }
    
    public void Execute()
    {
        var data = GetDataByType(_ObjectClass);
        if (!data.Any())
        {
            Console.WriteLine("Unknown Object Class");
            return;
        }
        var filteredData = FilterData(data, _Conditions);
        DisplaySelectedFields(filteredData, _Fields, _ObjectClass);
    }
    
    private IEnumerable<IBaseObject> GetDataByType(string objectClass)
    {
        return DataExtractor.ExtractDataByType(objectClass, _data);
    }

    
    private IEnumerable<IBaseObject> FilterData(IEnumerable<IBaseObject> data, string conditions)
    {
        data = DataFilter.FilterData(data, conditions);
        return data;
    }

    
    private void DisplaySelectedFields(IEnumerable<IBaseObject> data, List<string> fields, string objectClass)
    {
        fields = DataFieldsSelector.InitializeFields(fields, objectClass);
        Dictionary<string, int> columnWidths = CalculateColumnWidths(data, fields);
        List<Dictionary<string, string>> rows = GenerateRows(data, fields);
        PrintHeadersAndData(rows, fields, columnWidths);
    }
    
    private Dictionary<string, int> CalculateColumnWidths(IEnumerable<IBaseObject> data, List<string> fields)
    {
        var columnWidths = new Dictionary<string, int>();
        foreach (var field in fields)
        {
            columnWidths[field] = field.Length;
        }

        foreach (var item in data)
        {
            foreach (var field in fields)
            {
                var value = GetValue(item, field);
                if (value.Length > columnWidths[field])
                {
                    columnWidths[field] = value.Length;
                }
            }
        }
        return columnWidths;
    }
    
    private string GetValue(IBaseObject item, string field)
    {
        if (field == "Origin")
        {
            var airportID = _propertyAccessors.GetValue(item, "OriginID");
            var airports = DataExtractor.ExtractAirports(_data);
            var airport = airports.FirstOrDefault(a => a.ID.ToString() == airportID.ToString());
            return airport != null ? airport.Name : "Unknown Airport";
        }
        else if (field == "Target")
        {
            var airportID = _propertyAccessors.GetValue(item, "TargetID");
            var airports = DataExtractor.ExtractAirports(_data);
            var airport = airports.FirstOrDefault(a => a.ID.ToString() == airportID.ToString());
            return airport != null ? airport.Name : "Unknown Airport";
        }
        else if (field == "Plane")
        {
            var planeID = _propertyAccessors.GetValue(item, "PlaneID");
            var planes = DataExtractor.ExtractPlanes(_data);
            var plane = planes.FirstOrDefault(a => a.ID.ToString() == planeID.ToString());
            return plane != null ? plane.Serial : "Unknown Plane";
        }
        var value = _propertyAccessors.GetValue(item, field);
        return value.ToString();
    }
    
    private List<Dictionary<string, string>> GenerateRows(IEnumerable<IBaseObject> data, List<string> fields)
    {
        List<Dictionary<string, string>> rows = new List<Dictionary<string, string>>();
        foreach (var item in data)
        {
            var row = new Dictionary<string, string>();
            foreach (var field in fields)
            {
                var value = GetValue(item, field);
                row[field] = value;
            }
            rows.Add(row);
        }
        return rows;
    }


    private void PrintHeadersAndData(List<Dictionary<string, string>> rows, List<string> fields, Dictionary<string, int> columnWidths)
    {
        for (int i = 0; i < fields.Count; i++)
        {
            Console.Write($" {fields[i].PadRight(columnWidths[fields[i]])}");
            if (i < fields.Count - 1) Console.Write(" |");
        }
        Console.WriteLine();

        for (int i = 0; i < fields.Count; i++)
        {
            Console.Write(new string('-', columnWidths[fields[i]] + 2));
            if (i < fields.Count - 1) Console.Write("+");
        }
        Console.WriteLine();

        foreach (var row in rows)
        {
            for (int i = 0; i < fields.Count; i++)
            {
                string field = fields[i];
                var value = row[field];
                int padding = columnWidths[field] - value.Length;

                Console.Write(' ' + new string(' ', padding) + value);
        
                if (i < fields.Count - 1) Console.Write(" |");
            }
            Console.WriteLine();
        }
    }
}