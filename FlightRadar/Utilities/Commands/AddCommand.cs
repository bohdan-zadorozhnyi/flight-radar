namespace FlightRadar.Utilities.Commands;
using Factories;
using Interfaces;

public class AddCommand : ICommand
    {
        private List<IBaseObject> _data;
        private string _objectClass;
        private Dictionary<string, string> _properties;

        public AddCommand(List<IBaseObject> data, string objectClass, Dictionary<string, string> properties)
        {
            _data = data;
            _objectClass = objectClass;
            _properties = properties;
        }

        public void Execute()
        {
            var dataFactory = new QueryDataFactory();
            IBaseObject newObject = dataFactory.CreateObject(_objectClass, _properties);
            if (newObject == null)
            {
                throw new InvalidOperationException($"Unsupported object class: {_objectClass}");
            }

            _data.Add(newObject);
        }
    }