using Amega.Integration.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Amega.Integration.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InstrumentPriceController(ILogger<InstrumentPriceController> logger,
                                       IInstrumentPriceService priceService) : ControllerBase
{
    private readonly ILogger<InstrumentPriceController> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IInstrumentPriceService _priceService = priceService ?? throw new ArgumentNullException(nameof(priceService));

    [HttpGet("GetInstrumentPrice")]
    public async Task<IActionResult> Get(string symbol)
    {
        try
        {
            ArgumentException.ThrowIfNullOrEmpty(symbol);

            var res = await _priceService.GetPriceAsync(symbol);
            return res is null ? NotFound() : Ok(res);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Could not get fin instrument price");
            return BadRequest(ex.Message);
        }
    }
}
