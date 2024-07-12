using Amega.Integration.Api.Controllers;
using Amega.Integration.Api.Models;
using System.Linq;

namespace Amega.Integration.Api.Services;

// Farther we should create IFinInstrumentService implementation
// To get and store fin instruments on persistant storage.
public class FinInstrumentServiceMock : IFinInstrumentService
{
    private readonly ICollection<FinInstrument> _finInstruments;
    private readonly ILogger<FinInstrumentServiceMock> _logger;
    public FinInstrumentServiceMock(ILogger<FinInstrumentServiceMock> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _finInstruments = new List<FinInstrument>()
        {
             new("EURUSD", "Euro/US Dollar"),
             new("USDJPY", "US Dollar/Japanese Yen"),
             new("BTCUSD", "Bitcoin/US Dollar")
        };
    }

    public Task<IEnumerable<FinInstrument>> GetFinInstrumentsAsync() => Task.FromResult(_finInstruments.AsEnumerable());
    public Task<FinInstrument?> GetFinInstrumentsBySymbolAsync(string symbol) => Task.FromResult(_finInstruments.FirstOrDefault(x => x.Symbol == symbol));

    public Task<bool> AddFinInstrumentAsync(FinInstrument instrument)
    {
        ArgumentNullException.ThrowIfNull(instrument);

        if (_finInstruments.Any(x => x.Symbol == instrument.Symbol))
        {
            _logger.LogWarning($"Instrument with Symbol: {instrument.Symbol} already exist");
            return Task.FromResult(false);
        }

        _finInstruments.Add(instrument);
        return Task.FromResult(true);
    }

    public Task<bool> RemoveFinInstrumentAsync(string symbol)
    {
        ArgumentException.ThrowIfNullOrEmpty(symbol);

        var instrument = _finInstruments.FirstOrDefault(x => x.Symbol == symbol);
        if (instrument is null)
        {
            _logger.LogWarning($"Instrument with Symbol: {symbol} does not exist");
            return Task.FromResult(false);
        }

        _finInstruments.Remove(instrument);
        return Task.FromResult(true);
    }
}
