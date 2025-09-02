using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CryptoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoController : ControllerBase
    {
        private readonly CryptoPriceService _cryptoPriceService;

        public CryptoController(CryptoPriceService cryptoPriceService)
        {
            _cryptoPriceService = cryptoPriceService;
        }

        [HttpGet("{symbol}")]
        public async Task<ActionResult<decimal?>> GetCryptoPrice(string symbol)
        {
            var result = await _cryptoPriceService.GetPriceBySymbolAsync(symbol);

            if (result.IsSuccess)
            {
                if (result.Value.HasValue)
                {
                    return Ok(result.Value.Value);
                }
                return NotFound();
            }

            return StatusCode(502, result.Errors.Select(e => e.Message));
        }
    }
}