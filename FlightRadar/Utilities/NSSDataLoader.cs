using NetworkSourceSimulator;
using FlightRadar.Interfaces;
using FlightRadar.Models;
namespace FlightRadar.Utilities;

public class NSSDataLoader
{
    private readonly NetworkSourceSimulator.NetworkSourceSimulator source;
    private readonly List<IBaseObject> data;
    private Logger logger;
    
    private NSSDataLoader(string ftrFilePath, int minOffsetInMs, int maxOffsetInMs, List<IBaseObject> _data)
    {
        logger = new Logger();
        source = new NetworkSourceSimulator.NetworkSourceSimulator(ftrFilePath, minOffsetInMs, maxOffsetInMs);
        data = _data;
        source.OnNewDataReady += HandleNewDataReady;
        
        source.OnIDUpdate += HandleIDUpdate;
        source.OnPositionUpdate += HandlePositionUpdate;
        source.OnContactInfoUpdate += HandleContactInfoUpdate;
        
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
    
    private void HandleIDUpdate(object sender, IDUpdateArgs args)
    {
        if (args.ObjectID == args.NewObjectID)
        {
            return;
        }
        
        foreach (var item in data.OfType<IIDUpdateable>())
        {
            try
            {
                var baseObject = item as IBaseObject;
                if (baseObject != null && baseObject.ID == args.ObjectID)
                {
                    item.UpdateID(args.NewObjectID);

                    if (item is Crew)
                    {
                        DataUpdater.UpdateFlightCrewId(data, args.ObjectID, args.NewObjectID);
                    }
                    else if (item is ILoad)
                    {
                        DataUpdater.UpdateFlightLoadId(data, args.ObjectID, args.NewObjectID);
                    }
                    else if (item is CargoPlane || item is PassengerPlane)
                    {
                        DataUpdater.UpdateFlightPlaneID(data, args.ObjectID, args.NewObjectID);
                    }
                    
                    string logData = $"Object ID {args.ObjectID} changed to ID {args.NewObjectID}";
                    logger.LogChange(logData);
                }
            }
            catch (Exception ex)
            {
                logger.LogChange($"Failed to update object ID: {args.ObjectID} - Error: {ex.Message}");
            }
        }
    }

    private void HandlePositionUpdate(object sender, PositionUpdateArgs args)
    {
        foreach (var item in data.OfType<IPositionUpdateable>())
        {
            var baseObject = item as IBaseObject;
            var positinObject = item as IPositionUpdateable;
            
            try
            {
                if (baseObject != null && baseObject.ID == args.ObjectID)
                {
                    logger.LogChange($"Position update for Object ID: {baseObject.ID} - Original Position: " +
                                     $"(Long: {positinObject.Longitude}, Lat: {positinObject.Latitude}, AMSL: {positinObject.AMSL})");

                    item.UpdatePosition(args.Longitude, args.Latitude, args.AMSL);

                    logger.LogChange($"Position update for Object ID: {baseObject.ID} - Original Position: " +
                                     $"(Long: {positinObject.Longitude}, Lat: {positinObject.Latitude}, AMSL: {positinObject.AMSL})");
                }
            }
            catch (Exception ex)
            {
                logger.LogChange($"Failed to update position for object ID: {args.ObjectID} - Error: {ex.Message}");
            }
        }
    }

    private void HandleContactInfoUpdate(object sender, ContactInfoUpdateArgs args)
    {
        foreach (var item in data.OfType<IContactInfoUpdateable>())
        {
            var baseObject = item as IBaseObject;
            var contactInfoObject = item as IContactInfoUpdateable;
            
            try
            {
                if (baseObject != null && baseObject.ID == args.ObjectID)
                {
                    logger.LogChange(
                        $"Contact info update for Object ID: {baseObject.ID} - Original Phone: {contactInfoObject.Phone}, " +
                        $"Original Email: {contactInfoObject.Email}");

                    item.UpdateContactInfo(args.PhoneNumber, args.EmailAddress);

                    logger.LogChange(
                        $"Contact info update for Object ID: {baseObject.ID} - Updated Phone: {contactInfoObject.Phone}, " +
                        $"Updated Email: {contactInfoObject.Email}");
                }
            }
            catch (Exception ex)
            {
                logger.LogChange($"Failed to update contact info for object ID: {args.ObjectID} - Error: {ex.Message}");
            }
        }
    }

    public static void RunNSSDataLoader(string ftrFilePath, int minOffsetInMs, int maxOffsetInMs, List<IBaseObject> data)
    {
        NSSDataLoader network = new NSSDataLoader(ftrFilePath, minOffsetInMs, maxOffsetInMs, data);
    }
}