namespace FlightRadar.Utilities;
using System;
using System.IO;

public class Logger
{
    private readonly string logPath;
    private readonly StreamWriter logWriter;

    public Logger()
    {
        string directory = AppDomain.CurrentDomain.BaseDirectory;
        logPath = Path.Combine(directory, "Logs");
        Directory.CreateDirectory(logPath);

        string logFileName = $"log_{DateTime.Now.ToString("yyyy-MM-dd")}.txt";
        string logFilePath = Path.Combine(logPath, logFileName);
        logWriter = new StreamWriter(logFilePath, true)
        {
            AutoFlush = true
        };
        
        logWriter.WriteLine($"-------------------{DateTime.Now:yyyy-MM-dd HH:mm:ss}-------------------");
    }

    public void LogChange(string message)
    {
        logWriter.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
    }
}