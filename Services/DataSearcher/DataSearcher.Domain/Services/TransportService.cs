using System.Text.Json;
using DataSearcher.Domain.Helpers.Data.Parsers;
using DataSearcher.Domain.Helpers.Data.Providers;

namespace DataSearcher.Domain.Services;

public class TransportService
{
    private IDataProvider _dataProvider;
    public TransportService(IDataProvider provider = null)
    {
        _dataProvider = provider == null ? new WebScraper() : provider;
    }
    public string GetRouteStops(string routeId)
    {
        throw new NotImplementedException();
    }

    public string GetRouteStopShedule(string routeId, string stopName)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<string> GetRoutes()
    {
        // i know, it`s not right :)
        var routesVal = _dataProvider.GetRoutes();
        foreach  (var node in routesVal)
        {
            yield return node.Key;
        }
    }
}