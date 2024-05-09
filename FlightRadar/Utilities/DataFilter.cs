namespace FlightRadar.Utilities;
using Interfaces;

public class DataFilter
{
    private static readonly PropertyAccessors _propertyAccessors = new PropertyAccessors();
    
    public static IEnumerable<IBaseObject> FilterData(IEnumerable<IBaseObject> data, string conditions)
    {
        if (string.IsNullOrEmpty(conditions))
            return data;

        var orConditions = conditions.Split(new[] { " or " }, StringSplitOptions.RemoveEmptyEntries);
        List<IEnumerable<IBaseObject>> results = new List<IEnumerable<IBaseObject>>();
        foreach (var orCondition in orConditions)
        {
            IEnumerable<IBaseObject> partialResult = data;
            var andConditions = orCondition.Split(new[] { " and " }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var condition in andConditions)
            {
                var parts = ParseCondition(condition);
                if (parts == null)
                    continue;
                partialResult = partialResult.Where(item => EvaluateCondition(item, parts.Item1, parts.Item2, parts.Item3));
            }

            results.Add(partialResult);
        }

        var combinedResults = results.SelectMany(x => x).Distinct();
        return combinedResults;
    }

    
    private static Tuple<string, string, string> ParseCondition(string condition)
    {
        string[] operators = { "=", "!=", "<=", ">=", "<", ">" };
        foreach (var op in operators)
        {
            var index = condition.IndexOf(op);
            if (index != -1)
            {
                var field = condition.Substring(0, index).Trim();
                var value = condition.Substring(index + op.Length).Trim();
                return Tuple.Create(field, op, value);
            }
        }
        return null;
    }
    
    private static bool EvaluateCondition(IBaseObject item, string field, string op, string value)
    {
        string itemValueAsString;
        if (field == "WorldPosition.Lat" || field == "WorldPosition.Long")
        {
            var itemValue = _propertyAccessors.GetValue(item, "WorldPosition");
            Console.WriteLine(itemValue.ToString());
            if (itemValue is Structures.WorldPosition position)
            {
                itemValueAsString = (field == "WorldPosition.Lat" ? position.Lat : position.Long).ToString();
            }
            else
            {
                itemValueAsString = "Invalid WorldPosition";
            }
        }
        else
        {
            var itemValue = _propertyAccessors.GetValue(item, field);
            itemValueAsString = itemValue.ToString();
        }
        return CompareValues(itemValueAsString, value, op);
    }
    
    private static bool CompareValues(string itemValue, string conditionValue, string op)
    {
        switch (op)
        {
            case "=": return itemValue == conditionValue;
            case "!=": return itemValue != conditionValue;
            case "<": return Convert.ToDouble(itemValue) < Convert.ToDouble(conditionValue);
            case ">": return Convert.ToDouble(itemValue) > Convert.ToDouble(conditionValue);
            case "<=": return Convert.ToDouble(itemValue) <= Convert.ToDouble(conditionValue);
            case ">=": return Convert.ToDouble(itemValue) >= Convert.ToDouble(conditionValue);
            default: return false;
        }
    }
}