namespace FlightRadar.Utilities.Commands;
using Interfaces;

public class UnknownCommand : ICommand
{
    public void Execute()
    {
        Console.WriteLine("Unknown command. Please try again.");
    }
}