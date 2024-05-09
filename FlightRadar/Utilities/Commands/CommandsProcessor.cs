namespace FlightRadar.Utilities.Commands;
using Interfaces;
using System.Text.RegularExpressions;

public class CommandsProcessor
{
    private List<IBaseObject> data;
    private readonly JSONSerializer jsonSerializer;

    public CommandsProcessor(List<IBaseObject> _data)
    {
        data = _data;
        jsonSerializer = new JSONSerializer();
    }
    
    public void ProcessCommands()
    {
        while (true)
        {
            Console.WriteLine("Enter a command ('print', 'report', 'display', 'add', 'delete', 'update' or 'exit'): ");
            string commandInput = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(commandInput))
            {
                Console.WriteLine("Invalid command. Please try again.");
                continue;
            }
            
            ICommand command = GetCommand(commandInput);
            command.Execute();
        }
    }
    
    public ICommand GetCommand(string commandName)
    {
        switch (CommandClassifier(commandName))
        {
            case "print":
                return new PrintCommand(data, jsonSerializer);
            case "report":
                return new ReportCommand(data);
            case "exit":
                return new ExitCommand();
            case "display":
                var displayParser = new CommandParser(commandName);
                return new DisplayCommand(data, displayParser.ObjectClass, displayParser.Fields, displayParser.Conditions);
            case "delete":
                var deleteParser = new CommandParser(commandName);
                return new DeleteCommand(data, deleteParser.ObjectClass, deleteParser.Conditions);
            case "add":
                var addParser = new CommandParser(commandName);
                return new AddCommand(data, addParser.ObjectClass, addParser.Properties);
            case "update":
                var updateParser = new CommandParser(commandName);
                return new UpdateCommand(data, updateParser.ObjectClass, updateParser.Properties, updateParser.Conditions);
            default:
                return new UnknownCommand();
        }
    }
    
    public string CommandClassifier(string commandName)
    {
        if (Regex.IsMatch(commandName.ToLower(), @"^display\s.+"))
            return "display";
        else if (Regex.IsMatch(commandName.ToLower(), @"^delete\s.+"))
            return "delete";
        else if (Regex.IsMatch(commandName.ToLower(), @"^add\s.+"))
            return "add";
        else if (Regex.IsMatch(commandName.ToLower(), @"^update\s.+"))
            return "update";
        else
            return commandName.ToLower();
    }
}