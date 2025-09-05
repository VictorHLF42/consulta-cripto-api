using Application.Services;
using Domain.Interfaces;
using Infra.Data;
using Infra.ExternalServices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CryptoDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<DbContext, CryptoDbContext>();
builder.Services.AddScoped<ICryptoRepository, CryptoRepository>();
builder.Services.AddScoped<CryptoPriceService>();
builder.Services.AddHttpClient<CoinMarketCapClient>(client =>
{
    var coinMarketCapApiKey = builder.Configuration["CoinMarketCap:ApiKey"];
    client.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", coinMarketCapApiKey);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CryptoDbContext>();
    db.Database.SetCommandTimeout(1800);
    await db.Database.MigrateAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();