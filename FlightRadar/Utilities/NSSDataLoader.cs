using NetworkSourceSimulator;
using FlightRadar.Interfaces;
namespace FlightRadar.Utilities;

public class NSSDataLoader
{
    private readonly NetworkSourceSimulator.NetworkSourceSimulator source;
    private readonly List<IBaseObject> data;
    
    private NSSDataLoader(string ftrFilePath, int minOffsetInMs, int maxOffsetInMs, List<IBaseObject> _data)
    {
        source = new NetworkSourceSimulator.NetworkSourceSimulator(ftrFilePath, minOffsetInMs, maxOffsetInMs);
        data = _data;
        source.OnNewDataReady += HandleNewDataReady;
        
        StartThread();
    }
    
    private void StartThread()
    {
        Thread sourceThread = new Thread(source.Run);
        sourceThread.Start();
    }
    
    private void HandleNewDataReady(object sender, NewDataReadyArgs args)
    {
        Message message = source.GetMessageAt(args.MessageIndex);
        DataLoader.LoadMessage(data, message);
    }

    public static void RunNSSDataLoader(string ftrFilePath, int minOffsetInMs, int maxOffsetInMs, List<IBaseObject> data)
    {
        NSSDataLoader network = new NSSDataLoader(ftrFilePath, minOffsetInMs, maxOffsetInMs, data);
    }
}