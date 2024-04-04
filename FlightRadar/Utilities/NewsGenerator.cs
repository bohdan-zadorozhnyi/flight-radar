namespace FlightRadar.Utilities;
using Interfaces;
using Models.NewsProviders;

public class NewsGenerator
{
    private readonly List<INewsProvider> _newsProviders;
    private readonly List<IReportable> _reportables;

    public NewsGenerator(List<INewsProvider> newsProviders, List<IReportable> reportables)
    {
        _newsProviders = newsProviders;
        _reportables = reportables;
    }
    
    public void GenerateAllNews()
    {
        foreach (var newsPiece in GenerateNextNews())
        {
            Console.WriteLine(newsPiece);
        }
    }
    
    public IEnumerable<string> GenerateNextNews()
    {
        foreach (var newsProvider in _newsProviders)
        {
            foreach (var reportable in _reportables)
            {
                var newsPiece = reportable.Accept(newsProvider);
                yield return newsPiece;
            }
        }
    }
    
    public static List<INewsProvider> CreateNewsProvidersList()
    {
        var newsProviders = new List<INewsProvider>
        {
            new Television("Abelian Television"),
            new Television("Channel TV-Tensor"),
            new Radio("Quantifier radio"),
            new Radio("Shmem radio"),
            new Newspaper("Categories Journal"),
            new Newspaper("Polytechnical Gazette")
        };

        return newsProviders;
    }
}