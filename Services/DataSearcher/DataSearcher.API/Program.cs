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
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TransportRouteContext>(
    option => option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
    );

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();
app.Run();
