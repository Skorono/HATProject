using DataSearcher.Data.Model;
using DataSearcher.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Route = DataSearcher.Data.Model.Route;

namespace DataSearcher.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransportController : ControllerBase
{
    private readonly TransportService _service = new();

    [HttpGet("getRoutesAsync")]
    public async Task<List<Route>?> GetRoutesAsync()
    {
        return await _service.GetRoutesAsync();
    }

    [HttpGet("getRoutesByNameAsync")]
    public async Task<List<Route>?> GetRoutesByNameAsync(string routeName)
    {
        var routes = await _service.GetRoutesAsync();
        return routes?.Where(route => route.Name == routeName).ToList();
    }

    [HttpGet("getRouteStopsAsync")]
    public async Task<List<Stop>?> GetRouteStops(int routeId, DateTime date)
    {
        return await _service.GetRouteStopsAsync(routeId, DateOnly.Parse(date.ToShortDateString()));
    }

    [HttpGet("getRoutesStopsByName")]
    public async Task<List<List<Stop>?>?> GetRoutesStopsByNameAsync(string routeName, DateTime date)
    {
        var routes = await GetRoutesByNameAsync(routeName);
        return routes?.Select(route =>
            _service.GetRouteStopsAsync(route.Id, DateOnly.Parse(date.ToShortDateString())).Result).ToList();
    }
}