namespace FlightRadar.Interfaces;

public interface IReportable
{
    string Accept(INewsProvider visitor);
}