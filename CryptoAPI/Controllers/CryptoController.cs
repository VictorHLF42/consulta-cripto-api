using Application.Services;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<decimal?>> GetCryptoPrice(string symbol)
        {
            var price = await _cryptoPriceService.GetPriceBySymbolAsync(symbol);

            if (price == null)
            {
                // Retorna 404 Not Found se a moeda não for encontrada.
                return NotFound();
            }

            // Retorna 200 OK com o preço.
            return Ok(price);
        }
    }
}