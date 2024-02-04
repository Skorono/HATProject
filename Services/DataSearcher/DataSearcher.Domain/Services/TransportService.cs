using DataSearcher.Data.Model;
using DataSearcher.Domain.Helpers.Data.Providers;

namespace DataSearcher.Domain.Services;

public class TransportService
{
    private readonly IDataProvider _dataProvider;

    public TransportService(IDataProvider? provider = null)
    {
        _dataProvider = provider ?? new WebScraper();
    }

    public async Task<List<Stop>?> GetRouteStopsAsync(int routeId, DateOnly? date = null)
    {
        return await new TaskFactory().StartNew(() => _dataProvider.GetStops(routeId, date));
    }

    public string GetRouteStopShedule(string routeId, string stopName)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Route>?> GetRoutesAsync()
    {
        return await new TaskFactory().StartNew(() => _dataProvider.GetRoutes()!);
    }
}