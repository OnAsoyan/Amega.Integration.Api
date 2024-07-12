using Amega.Integration.Api.Models;

namespace Amega.Integration.Api.Services;

public interface IInstrumentPriceService
{
    Task<FinData?> GetPriceAsync(string instrument);
}
