using DataSearcher.Data.Context;
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

foreach (var route in new TransportService().GetRoutes())
{
    Console.WriteLine(route);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();
