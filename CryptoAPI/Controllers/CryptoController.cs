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
        public async Task<ActionResult> GetCryptoPrice(string symbol)
        {
            var result = await _cryptoPriceService.GetPriceBySymbolAsync(symbol);

            if (!result.IsSuccess)
            {
                return StatusCode(502, result.Errors.Select(e => e.Message));
            }

            var response = new
            {
                Symbol = symbol,
                Price = result.Value,
                UpdatedAt = DateTime.UtcNow
            };

            return Ok(response);
        }
    }
}
