namespace FlightRadar.Utilities.Commands;
using System.Text.RegularExpressions;

public class CommandParser
{
    public string ObjectClass { get; private set; }
    public List<string> Fields { get; private set; }
    public string Conditions { get; private set; }
    public Dictionary<string, string> Properties { get; private set; }

    public CommandParser(string command)
    {
        Parse(command);
    }

    private void Parse(string command)
    {
        string displayPattern = @"display\s+([\w,*,\s]+)\s+from\s+(\w+)(?:\s+where\s+(.+))?";
        string deletePattern = @"delete\s+(\w+)(?:\s+where\s+(.+))?";
        string addPattern = @"add (\w+) new \(([^)]+)\)";
        string updatePattern = @"update (\w+) set \(([^)]+)\)(?: where (.+))?";
        var displayMatch = Regex.Match(command, displayPattern, RegexOptions.IgnoreCase);
        if (displayMatch.Success)
        {
            Fields = displayMatch.Groups[1].Value.Split(',').Select(f => f.Trim()).ToList();
            ObjectClass = displayMatch.Groups[2].Value.Substring(0, displayMatch.Groups[2].Value.Length - 1);
            Conditions = displayMatch.Groups[3].Value;
            return;
        }
        
        var deleteMatch = Regex.Match(command, deletePattern, RegexOptions.IgnoreCase);
        if (deleteMatch.Success)
        {
            ObjectClass = deleteMatch.Groups[1].Value.Substring(0, deleteMatch.Groups[1].Value.Length - 1);
            Conditions = deleteMatch.Groups[2].Value;
            return;
        }
        
        var addMatch = Regex.Match(command, addPattern, RegexOptions.IgnoreCase);
        if (addMatch.Success)
        {
            ObjectClass = addMatch.Groups[1].Value.Substring(0, addMatch.Groups[1].Value.Length - 1);
            Properties = addMatch.Groups[2].Value
                .Split(',')
                .Select(p => p.Trim().Split('='))
                .ToDictionary(p => p[0].Trim(), p => p[1].Trim());
            return;
        }
        
        var updateMatch = Regex.Match(command, updatePattern, RegexOptions.IgnoreCase);
        if (updateMatch.Success)
        {
            ObjectClass = updateMatch.Groups[1].Value.Substring(0, updateMatch.Groups[1].Value.Length - 1);
            Properties = updateMatch.Groups[2].Value
                .Split(',')
                .Select(p => p.Trim().Split('='))
                .ToDictionary(p => p[0].Trim(), p => p[1].Trim());
            Conditions = updateMatch.Groups[3].Value;
        }
    }
}