using System.Text.Json;
using DataSearcher.Data.Model;
using DataSearcher.Domain.Helpers.Data.Parsers;
using DataSearcher.Domain.Helpers.Data.Providers;

namespace DataSearcher.Domain.Services;

public class TransportService
{
    private IDataProvider _dataProvider;
    public TransportService(IDataProvider? provider = null)
    {
        _dataProvider = provider ?? new WebScraper();
    }

    public async Task<List<Stop>?> GetRouteStopsAsync(int routeId) => await new TaskFactory().StartNew(() => _dataProvider.GetStops(routeId));

    public string GetRouteStopShedule(string routeId, string stopName)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Route>?> GetRoutesAsync() => await new TaskFactory().StartNew(() => _dataProvider.GetRoutes()!);
}