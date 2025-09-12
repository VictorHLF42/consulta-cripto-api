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
            var result = await _cryptoPriceService.GetPriceBySymbolAsync(symbol);

            if (!result.IsSuccess)
            {
                // O desafio requer um 502 Bad Gateway para criptomoedas não encontradas.
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

        // --- NOVO ENDPOINT PARA HISTÓRICO DE PREÇOS ---

        [HttpGet("history")]
        public async Task<ActionResult> GetHistory([FromQuery] string symbol, [FromQuery] DateTime? dateFrom, [FromQuery] DateTime? dateTo)
        {
            // O parâmetro 'symbol' é obrigatório.
            if (string.IsNullOrWhiteSpace(symbol))
            {
                return BadRequest("O parâmetro 'symbol' é obrigatório.");
            }

            var result = await _cryptoPriceService.GetHistoryBySymbolAsync(symbol, dateFrom, dateTo);

            if (!result.IsSuccess)
            {
                // O desafio requer um 404 Not Found se nenhum registro for encontrado.
                return NotFound(result.Errors.Select(e => e.Message));
            }

            return Ok(result.Value);
        }
    }
}