using Domain.Interfaces;
using Infra.Data;
using Infra.Data.Repositories;
using Infra.ExternalServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Infra.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CryptoDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<ICryptoRepository, CryptoRepository>();
            services.AddHttpClient<CoinMarketCapClient>(client =>
            {
                var coinMarketCapApiKey = configuration["CoinMarketCap:ApiKey"];
                client.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", coinMarketCapApiKey);
            });

            return services;
        }
    }
}