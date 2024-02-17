using Microsoft.AspNetCore.Mvc;
using Transports.Data.Context;
using Route = Transports.Data.Model;

namespace DataSearcher.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransportController : ControllerBase
{
    private readonly TransportRouteContext _context;
    private ILogger<TransportController> _logger;

    public TransportController(TransportRouteContext context, ILogger<TransportController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("getRouteById")]
    public async Task<Route?> GetRouteById(int routeId)
    {
        var routes = await GetRoutesAsync();
        return routes?.First(route => route.Id == routeId);
    }

    [HttpGet("getRoutesAsync")]
    public async Task<List<Route>?> GetRoutesAsync()
    {
        var cachedResult = _cacheManager.GetResponsesByHeader<Route>("route");

        if (cachedResult == null || !cachedResult.Any())
        {
            var dbResult = _context.Routes.ToList();
            dbResult.ForEach(r => _cacheManager.AddResponse(
                new("route", r.Id),
                new ResponseUnit<Route>
                {
                    Data = new List<Route> { r },
                    LifeTime = TimeSpan.FromMinutes(1)
                }));

            if (!dbResult.Any())
            {
                dbResult = await _service.GetRoutesAsync();
                dbResult?.ForEach(route => _context.Routes.Add(route));
                await _context.SaveChangesAsync();
            }

            return dbResult;
        }

        return cachedResult.Select(r => r?.Data.First()).ToList();
    }

    [HttpGet("getRoutesByNameAsync")]
    public async Task<List<Route>?> GetRoutesByNameAsync(string routeName)
    {
        var routes = await GetRoutesAsync();
        return routes?.Where(route => route.Name == routeName).ToList();
    }

    [HttpGet("getRouteStopsAsync")]
    public async Task<List<Route.Stop>?> GetRouteStops(int routeId, DateTime date)
    {
        var stopBindings =
            _context.RouteStopBindings.Where(binding => binding.RouteId == routeId);
        if (!stopBindings.Any())
        {
            var stops = await _service.GetRouteStopsAsync(routeId);
            stops?.ForEach(stop =>
            {
                if (!_context.Stops.Contains(stop))
                    _context.Stops.Add(stop);
                _context.RouteStopBindings.Add(new Route.RouteStopsBinding
                {
                    RouteId = routeId,
                    StopId = stop.Id
                });
            });
            await _context.SaveChangesAsync();
        }

        return _context.RouteStopBindings
            .Where(binding => binding.RouteId == routeId)
            .Select(binding => _context.Stops.First(stop => stop.Id == binding.StopId)).ToList();
    }

    [HttpGet("getRoutesStopsByName")]
    public async Task<List<List<Route.Stop>?>?> GetRoutesStopsByNameAsync(string routeName, DateTime date)
    {
        var routes = await GetRoutesByNameAsync(routeName);
        return routes?.Select(route =>
            GetRouteStops(route.Id, date).Result).ToList();
    }

    [HttpGet("getRouteSchedule")]
    public List<Route.Schedule>? GetSchedule(int routeId, DateTime date)
    {
        return _service.GetRouteStopShedule(routeId, DateOnly.Parse(date.ToShortDateString()));
    }
}