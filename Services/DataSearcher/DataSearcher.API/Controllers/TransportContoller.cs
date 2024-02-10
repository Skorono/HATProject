using DataSearcher.Data.Context;
using DataSearcher.Data.Model;
using DataSearcher.Domain.Helpers.Data.Providers;
using DataSearcher.Domain.Services;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Route = DataSearcher.Data.Model.Route;

namespace DataSearcher.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransportController : ControllerBase
{
    private TransportRouteContext _context;
    private readonly TransportService<HtmlDocument> _service = new(new WebScraper());

    public TransportController(TransportRouteContext context)
    {
        _context = context;
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
        var result = _context.Routes.ToList();
        if (!result.Any())
        {
            result = await _service.GetRoutesAsync();
            result.ForEach(route => _context.Routes.Add(route));
            await _context.SaveChangesAsync();
        }
        return result;
    }

    [HttpGet("getRoutesByNameAsync")]
    public async Task<List<Route>?> GetRoutesByNameAsync(string routeName)
    {
        var routes = await GetRoutesAsync();
        return routes?.Where(route => route.Name == routeName).ToList();
    }

    [HttpGet("getRouteStopsAsync")]
    public async Task<List<Stop>?> GetRouteStops(int routeId, DateTime date)
    {
        var stopBindings = 
            _context.RouteStopBindings.Where(binding => binding.RouteId == routeId);
        if (!stopBindings.Any())
        {
            var stops = await _service.GetRouteStopsAsync(routeId);
            stops?.ForEach(stop => { 
                if (!_context.Stops.Contains(stop)) 
                    _context.Stops.Add(stop);
                _context.RouteStopBindings.Add(new RouteStopsBinding()
                {
                    RouteId = routeId,
                    StopId = stop.Id
                });
            });
            await _context.SaveChangesAsync();
        }
        return _context.RouteStopBindings
            .Where(binding => binding.RouteId == routeId).ToList()
            .Select(binding => _context.Stops.First(stop => stop.Id == binding.StopId)).ToList();
    }

    [HttpGet("getRoutesStopsByName")]
    public async Task<List<List<Stop>?>?> GetRoutesStopsByNameAsync(string routeName, DateTime date)
    {
        var routes = await GetRoutesByNameAsync(routeName);
        return routes?.Select(route =>
            GetRouteStops(route.Id, date).Result).ToList();
    }

    [HttpGet("getRouteSchedule")]
    public List<Schedule>? GetSchedule(int routeId, DateTime date) =>
        _service.GetRouteStopShedule(routeId, DateOnly.Parse(date.ToShortDateString()));
}