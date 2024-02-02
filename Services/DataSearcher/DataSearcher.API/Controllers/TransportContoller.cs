using DataSearcher.Data.Model;
using DataSearcher.Domain.Services;
using MassTransit.Initializers;
using Microsoft.AspNetCore.Mvc;
using Route = DataSearcher.Data.Model.Route;

namespace DataSearcher.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransportContoller: ControllerBase
{
    private TransportService _service = new();

    [HttpGet("getRoutesAsync")]
    public async Task<List<DataSearcher.Data.Model.Route>?> GetRoutes() => 
        await _service.GetRoutesAsync();

    [HttpGet("getRoutesByNameAsync")]
    public async Task<List<Route>?> GetRoutesByNameAsync(string routeName)
    {
        var routes = await _service.GetRoutesAsync();
        return routes?.Where(route => route.Name == routeName).ToList();
    }

    [HttpGet("getRouteStopsAsync")]
    public async Task<List<Stop>?> GetRouteStops(int routeId) =>
        await _service.GetRouteStopsAsync(routeId);

    [HttpGet("getRoutesStopsByName")]
    public async Task<List<List<Stop>?>?> GetRoutesStopsByNameAsync(string routeName)
    {
        var routes = await GetRoutesByNameAsync(routeName);
        return routes?.Select(route => _service.GetRouteStopsAsync(route.Id).Result).ToList();
    }
}