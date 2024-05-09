namespace FlightRadar.Utilities.Commands;
using Interfaces;
using Factories;

public class UpdateCommand : ICommand
{
    private List<IBaseObject> _data;
    private string _objectClass;
    private Dictionary<string, string> _updates;
    private string _conditions;
    private readonly PropertyAccessors _propertyAccessors = new PropertyAccessors();

    public UpdateCommand(List<IBaseObject> data, string objectClass, Dictionary<string, string> updates, string conditions)
    {
        _data = data;
        _objectClass = objectClass;
        _updates = updates;
        _conditions = conditions;
    }

    public void Execute()
    {
        var objectsToUpdate = DataExtractor.ExtractDataByType(_objectClass, _data);
        if (!objectsToUpdate.Any())
        {
            Console.WriteLine("Unknown Object Class");
            return;
        }
        var filteredDataToUpdate = DataFilter.FilterData(objectsToUpdate, _conditions);
        UpdateProperties(filteredDataToUpdate, _updates);
    }

    private void UpdateProperties(IEnumerable<IBaseObject> data, Dictionary<string, string> updates)
    {
        foreach (var obj in data)
        {
            foreach (var update in updates)
            {
                _propertyAccessors.SetValue(obj, update.Key, update.Value);
            }
        }
    }
}