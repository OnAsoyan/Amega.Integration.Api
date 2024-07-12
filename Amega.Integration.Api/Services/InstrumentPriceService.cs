using Amega.Integration.Api.Models;

namespace Amega.Integration.Api.Services;

public class InstrumentPriceService(IHttpClientFactory httpClientFactory,
                                    ILogger<InstrumentPriceService> logger) : IInstrumentPriceService
{
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
    private readonly ILogger<InstrumentPriceService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<FinData?> GetPriceAsync(string instrument)
    {
        try
        {
            var client = _httpClientFactory.CreateClient("Tiingo");
            var uri = client.BaseAddress + instrument;
            var res = await client.GetFromJsonAsync<IEnumerable<FinData>>(uri);
            if (res is null || !res.Any())
            {
                _logger.LogInformation($"Could not find price data for {instrument} instrument");
                return null;
            }

            return res.FirstOrDefault();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured while getting price data from Tiingo");
            throw;
        }
    }
}
