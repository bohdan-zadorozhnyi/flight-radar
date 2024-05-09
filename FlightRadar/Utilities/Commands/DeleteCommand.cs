namespace FlightRadar.Utilities.Commands;
using Interfaces;

public class DeleteCommand : ICommand
{
    private List<IBaseObject> _data;
    private string _objectClass;
    private string _conditions;

    public DeleteCommand(List<IBaseObject> data, string objectClass, string conditions)
    {
        _data = data;
        _objectClass = objectClass;
        _conditions = conditions;
    }

    public void Execute()
    {
        var data = DataExtractor.ExtractDataByType(_objectClass, _data);
        if (!data.Any())
        {
            Console.WriteLine("Unknown Object Class");
            return;
        }
        
        IEnumerable<IBaseObject> toDelete = DataFilter.FilterData(data, _conditions);

        foreach (var item in toDelete.ToList())
        {
            _data.Remove(item);
        }
    }
}