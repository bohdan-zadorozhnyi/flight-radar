using FlightRadar.Interfaces;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace FlightRadar.Utilities;

public class FieldsJsonConverter : JsonConverter<IBaseObject>
{
    public override IBaseObject Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, IBaseObject value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, (object)value, options);
    }
}

public class JSONSerializer: IJSONSerializer
{
    public string Serialize(List<IBaseObject> objects)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Converters = { new FieldsJsonConverter() }
        };

        string serializedData = "";
        try
        {
            serializedData = JsonSerializer.Serialize(objects, options);
        }
        catch (Exception e)
        {
            throw new Exception("Error occurred during serialization: " + e.Message);
        }
        return serializedData;
    }
}