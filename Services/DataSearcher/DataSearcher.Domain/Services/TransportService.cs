using System.Text.Json;
using DataSearcher.Domain.Helpers.Providers;

namespace DataSearcher.Domain.Services;

public class TransportService: ITransportParser
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
        var routesVal = JsonSerializer.Deserialize<List<List<string>>>( _dataProvider.GetRoutes()["routes"]!.GetValue<string>());
        foreach  (var node in routesVal)
        {
            foreach (var name in node) yield return name;
        }
    }
}