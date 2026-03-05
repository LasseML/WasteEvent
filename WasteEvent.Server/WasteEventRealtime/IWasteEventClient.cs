namespace WasteEvent.Server.WasteEventRealtime
{
    public interface IWasteEventClient
    {
        Task WasteEventUpdated(WasteEvent wasteEvent);
    }
}
