using Microsoft.AspNetCore.SignalR;

namespace WasteEvent.Server.WasteEventRealtime
{
    public sealed class WasteEventHub : Hub<IWasteEventClient>
    {
    }
}
