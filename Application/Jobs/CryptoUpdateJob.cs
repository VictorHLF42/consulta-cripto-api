using Quartz;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Application.Services;
using System.Threading.Tasks;
using System.Linq; 
using System.Collections.Generic;
using System.Text;

namespace Application.Jobs
{
    public class CryptoUpdateJob : IJob
    {
        private readonly ILogger<CryptoUpdateJob> _logger;
        private readonly CryptoPriceService _cryptoPriceService;
        private readonly IConfiguration _configuration;
        private string symbol;

        public CryptoUpdateJob(ILogger<CryptoUpdateJob> logger, CryptoPriceService cryptoPriceService, IConfiguration configuration)
        {
            _logger = logger;
            _cryptoPriceService = cryptoPriceService;
            _configuration = configuration;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Job de atualização de preços de criptomoedas iniciado.");

            var cryptoSymbols = _configuration.GetSection("CryptoSettings:Symbols").Get<List<string>>();

            if (cryptoSymbols == null || !cryptoSymbols.Any())
            {
                _logger.LogWarning("Nenhum símbolo de criptomoeda foi encontrado no appsettings.json.");
                return;
            }
            var cryptos = cryptoSymbols.Aggregate((x,y) => x + "," + y);
            //var a = string.Join(",", cryptoSymbols);

            try
            {
                await _cryptoPriceService.UpdateCryptoPriceAsync(symbol);
                _logger.LogInformation($"Preço da criptomoeda {symbol} atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ocorreu um erro inesperado ao atualizar o preço da criptomoeda {symbol}.");
            }
        }
    }
}