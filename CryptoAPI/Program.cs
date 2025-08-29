using Infra.Data;
using Domain.Interfaces;
using Application.ExternalServices;
using Application.Services;
using Microsoft.EntityFrameworkCore;
using System.Net.Http; 
using FluentResults; 

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<CryptoDbContext>(options => options.UseSqlite("Data Source=crypto.db"));
builder.Services.AddHttpClient<CoinMarketCapClient>();
builder.Services.AddScoped<ICryptoRepository, CryptoRepository>();
builder.Services.AddScoped<CryptoPriceService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();