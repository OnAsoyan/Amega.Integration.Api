using Amega.Integration.Api.Models;
using Amega.Integration.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Amega.Integration.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FinancialInstrumentsController(ILogger<FinancialInstrumentsController> logger,
                                                IFinInstrumentService finInstrumentService) : ControllerBase
    {
        private readonly ILogger<FinancialInstrumentsController> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        private readonly IFinInstrumentService _finInstrumentService = finInstrumentService ?? throw new ArgumentNullException(nameof(finInstrumentService));

        [HttpGet("GetFinancialInstruments")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var res = await _finInstrumentService.GetFinInstrumentsAsync();
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Could not get fin instruments");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetBySymbol")]
        public async Task<IActionResult> GetBySymbol(string symbol)
        {
            try
            {
                var res = await _finInstrumentService.GetFinInstrumentsBySymbolAsync(symbol);
                return res == null ? NotFound() : Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Could not get fin instrument bt symbol {symbol}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddFinancialInstruments")]
        public async Task<IActionResult> Insert(FinInstrument instrument)
        {
            try
            {
                var res = await _finInstrumentService.AddFinInstrumentAsync(instrument);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Could not add fin instrument {instrument}");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("RemoveFinancialInstruments")]
        public async Task<IActionResult> Remove(string symbol)
        {
            try
            {
                var res = await _finInstrumentService.RemoveFinInstrumentAsync(symbol);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Could not remove fin instrument by symbol {symbol}");
                return BadRequest(ex.Message);
            }
        }
    }
}
