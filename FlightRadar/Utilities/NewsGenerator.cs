namespace FlightRadar.Utilities;
using Interfaces;
using Models.NewsProviders;

public class NewsGenerator
{
    private readonly List<INewsProvider> _newsProviders;
    private readonly List<IReportable> _reportables;
    private int _currentProviderIndex = 0;
    private int _currentReportableIndex = 0;

    public NewsGenerator(List<INewsProvider> newsProviders, List<IReportable> reportables)
    {
        _newsProviders = newsProviders;
        _reportables = reportables;
    }
    
    public void GenerateAllNews()
    {
        string newsPiece;
        while ((newsPiece = GenerateNextNews()) != null)
        {
            Console.WriteLine(newsPiece);
        }
        
        _currentProviderIndex = 0;
        _currentReportableIndex = 0;
    }

    public string GenerateNextNews()
    {
        if (_currentProviderIndex < _newsProviders.Count && _currentReportableIndex < _reportables.Count)
        {
            var newsProvider = _newsProviders[_currentProviderIndex];
            var reportable = _reportables[_currentReportableIndex];
            var newsPiece = reportable.Accept(newsProvider);

            _currentReportableIndex++;
            if (_currentReportableIndex >= _reportables.Count)
            {
                _currentReportableIndex = 0;
                _currentProviderIndex++;
            }

            return newsPiece;
        }

        return null;
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