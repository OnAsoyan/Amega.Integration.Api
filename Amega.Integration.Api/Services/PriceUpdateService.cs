
using Amega.Integration.Api.Models;
using Binance.Spot;
using Newtonsoft.Json;
using WebSocketManager = Amega.Integration.Api.Socket.WebSocketManager;

namespace Amega.Integration.Api.Services;

//We  will use background hosted service  to receive price real time data from binance
//this will keep only one connection between our server  and binance server
//and the clietns will be handled only by our service
//this will allow us avoid multiple connections with provider
public class PriceUpdateService(ILogger<FinInstrumentServiceMock> logger) : BackgroundService
{
    private readonly MarketDataWebSocket _binanceWebSocket = new("btcusdt@aggTrade");
    private readonly ILogger<FinInstrumentServiceMock> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            _binanceWebSocket.OnMessageReceived(
                async (data) =>
                {
                    var aggData = JsonConvert.DeserializeObject<AggTrade>(data);
                    var compactModel = new
                    {
                        Symbol = aggData.s,
                        Price = aggData.p
                    };
                     
                    await WebSocketManager.BroadcastMessageAsync(JsonConvert.SerializeObject(compactModel));
                }, stoppingToken);

            await _binanceWebSocket.ConnectAsync(stoppingToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occured while receiving live data from Binance");
            throw;
        }
    }

    public override void Dispose()
    {
        base.Dispose();
        _binanceWebSocket?.Dispose();
    }
}
