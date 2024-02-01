using System.Text.Json;
using DataSearcher.Data.Model;
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

    public List<Stop>? GetRouteStops(int routeId) => _dataProvider.GetStops(routeId);

    public string GetRouteStopShedule(string routeId, string stopName)
    {
        throw new NotImplementedException();
    }

    public List<Route>? GetRoutes() => _dataProvider.GetRoutes();
}