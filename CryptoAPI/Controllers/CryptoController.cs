using Application.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var result = await _cryptoPriceService.GetPriceBySymbolAsync(symbol.ToUpperInvariant());

            if (!result.IsSuccess)
            {
                return StatusCode(502, result.Errors.Select(e => e.Message));
            }

            var response = new
            {
                Symbol = result.Value.Symbol,
                Price = result.Value.Price,
                UpdatedAt = result.Value.CreatedAt
            };

            return Ok(response);
        }

        [HttpGet("history")]
        public async Task<ActionResult> GetHistory([FromQuery] string symbol, [FromQuery] DateTime? dateFrom, [FromQuery] DateTime? dateTo)
        {
            if (string.IsNullOrWhiteSpace(symbol))
            {
                return BadRequest("O parâmetro 'symbol' é obrigatório.");
            }

            var result = await _cryptoPriceService.GetHistoryBySymbolAsync(symbol.ToUpperInvariant(), dateFrom, dateTo);

            if (!result.IsSuccess)
            {
                return NotFound(result.Errors.Select(e => e.Message));
            }

            return Ok(result.Value);
        }
    }
}
