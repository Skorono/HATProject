using System.Text.Json.Serialization;
using DataSearcher.API.BackgroundServices;
using DataSearcher.Api.Managers;
using DataSearcher.Domain.Services;
using HtmlAgilityPack;
using MassTransit;

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

builder.Services.AddMassTransit(x =>
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(new Uri("rabbitmq://host.docker.internal/"));
        /*cfg.ConfigureJsonSerializerOptions(settings =>
        {
            settings.PreferredObjectCreationHandling = 
        });*/
    })
);

builder.Services.AddSingleton<CacheManager>();
builder.Services.AddScoped<IDataProvider<HtmlDocument>, WebScraperService>();

builder.Services.AddHostedService<MosTransScraperTimedHostedService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();
app.Run();