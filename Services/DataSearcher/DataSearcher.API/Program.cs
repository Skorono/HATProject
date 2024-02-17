using DataSearcher.Data.Context;
using DataSearcher.Domain.Helpers.Data.Providers;
using DataSearcher.Domain.Services;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost";
    options.InstanceName = "local";
});

builder.Services.AddDbContext<TransportRouteContext>(
    option => 
        option.UseNpgsql(builder.Configuration
            .GetConnectionString("DefaultConnection"))
);

builder.Services.AddSingleton<CacheManager>();
builder.Services.AddScoped<IDataProvider<HtmlDocument>, WebScraper>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();
app.Run();