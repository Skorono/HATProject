using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using DataSearcher.Data.Context;
using DataSearcher.Data.Model;
using DataSearcher.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Route = DataSearcher.Data.Model.Route;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TransportRouteContext>(
    option => option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
    );

var app = builder.Build();

var service = new TransportService();
var routes = service.GetRoutes().WaitAsync(TimeSpan.FromMinutes(10)).Result;
List<Task> tasks = new();
List<Task<Stop>> stops = new();

Console.WriteLine($"Маршруты: {routes.Count}");
foreach (var route in routes)
    service.GetRouteStops(route.Id).WaitAsync(TimeSpan.FromMinutes(10)).Result?.ForEach(stopList => Console.WriteLine(stopList?.Name));


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();

public class Test
{
    [Benchmark]
    public void GetSchedule()
    {
        
    }
}