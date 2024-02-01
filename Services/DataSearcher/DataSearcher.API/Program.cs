using DataSearcher.Data.Context;
using DataSearcher.Data.Model;
using DataSearcher.Domain.Services;
using Microsoft.EntityFrameworkCore;

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
List<Stop> stops = new();

foreach (var route in service.GetRoutes())
{
    var data = service.GetRouteStops(route.Id);
    data?.ForEach(stop => stops.Add(stop));
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();
