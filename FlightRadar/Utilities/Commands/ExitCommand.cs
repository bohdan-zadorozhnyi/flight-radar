namespace FlightRadar.Utilities.Commands;
using Interfaces;

public class ExitCommand : ICommand
{
    public void Execute()
    {
        Environment.Exit(0);
    }
}