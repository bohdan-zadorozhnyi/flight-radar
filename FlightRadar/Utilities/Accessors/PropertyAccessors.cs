namespace FlightRadar.Utilities;
using Interfaces;

public class PropertyAccessors
{
    private Dictionary<Type, Dictionary<string, Func<IBaseObject, object>>> accessors = new Dictionary<Type, Dictionary<string, Func<IBaseObject, object>>>();
    private Dictionary<Type, Dictionary<string, Action<IBaseObject, Func<object>>>> setters = new Dictionary<Type, Dictionary<string, Action<IBaseObject, Func<object>>>>();

    public PropertyAccessors()
    {
        RegisterAccessors();
        RegisterSetters();
    }

    private void RegisterAccessors()
    {
        Getters.RegisterAirportAccessors(accessors);
        Getters.RegisterFlightAccessors(accessors);
        Getters.RegisterCargoAccessors(accessors);
        Getters.RegisterCargoPlaneAccessors(accessors);
        Getters.RegisterCrewAccessors(accessors);
        Getters.RegisterPassengerAccessors(accessors);
        Getters.RegisterPassengerPlaneAccessors(accessors);
    }

    private void RegisterSetters()
    {
        Setters.RegisterAirportSetters(setters);
        Setters.RegisterFlightSetters(setters);
        Setters.RegisterCargoSetters(setters);
        Setters.RegisterCargoPlaneSetters(setters);
        Setters.RegisterCrewSetters(setters);
        Setters.RegisterPassengerSetters(setters);
        Setters.RegisterPassengerPlaneSetters(setters);
    }

    public object GetValue(IBaseObject item, string propertyName)
    {
        if (accessors.TryGetValue(item.GetType(), out var properties) && properties.TryGetValue(propertyName, out var accessor))
        {
            return accessor(item);
        }
        return "Property not accessible";
    }
    
    public void SetValue(IBaseObject item, string propertyName, object value)
    {
        if (setters.TryGetValue(item.GetType(), out var properties) && properties.TryGetValue(propertyName, out var setter))
        {
            setter(item, () => value);
        }
        else
        {
            throw new ArgumentException("Property not accessible or type mismatch");
        }
    }
}
