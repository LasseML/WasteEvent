using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;

namespace WasteEvent.Server.WasteEventRealtime
{
    internal sealed class WasteEventUpdater(
            IHubContext<WasteEventHub, IWasteEventClient> hubContext,
            IOptions<WasteEventUpdateOptions> options,
            WasteEventService wasteEventService,
            ILogger<WasteEventUpdater> logger)
        : BackgroundService
    {
        private readonly WasteEventUpdateOptions _options = options.Value;
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try 
            {             
                await foreach(WasteEvent wasteEvent in wasteEventService.Listen(_options.speedMultiplier, cancellationToken: stoppingToken))
                {
                    await hubContext.Clients.All.WasteEventUpdated(wasteEvent);
                }
            }
            catch (Exception ex) 
            {
                logger.LogError(ex, $"WasteEventUpdateOptions encountered an error: {ex.Message}");
            }
        }
    }
}
