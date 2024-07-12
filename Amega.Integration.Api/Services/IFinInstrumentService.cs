using Amega.Integration.Api.Models;

namespace Amega.Integration.Api.Services;

public interface IFinInstrumentService
{
    Task<IEnumerable<FinInstrument>> GetFinInstrumentsAsync();
    Task<FinInstrument?> GetFinInstrumentsBySymbolAsync(string symbol);
    Task<bool> AddFinInstrumentAsync(FinInstrument instrument);
    Task<bool> RemoveFinInstrumentAsync(string symbol);
}
