namespace FlightRadar.Utilities.Commands;
using Interfaces;

public class ReportCommand : ICommand
{
    private readonly List<IBaseObject> data;
    
    public ReportCommand(List<IBaseObject> data)
    {
        this.data = data;
    }
    
    public void Execute()
    {
        GenerateReport();
    }
    
    private void GenerateReport()
    {
        try
        {
            List<IBaseObject> snapshot;
            lock (data)
            {
                snapshot = data;
            }
            
            var newsProviders = NewsGenerator.CreateNewsProvidersList();
            var reportables = DataExtractor.ExtractReportables(snapshot);
            var newsGenerator = new NewsGenerator(newsProviders, reportables);
            newsGenerator.GenerateAllNews();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred while generating a report: {ex.Message}");
        }
    }
}