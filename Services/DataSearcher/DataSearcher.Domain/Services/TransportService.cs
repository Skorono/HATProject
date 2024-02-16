using DataSearcher.Data.Interfaces;
using DataSearcher.Data.Model;
using DataSearcher.Domain.Helpers;
using DataSearcher.Domain.Helpers.Data;
using DataSearcher.Domain.Helpers.Data.Providers;

namespace DataSearcher.Domain.Services;

public class TransportService<T>
{
    private readonly IDataProvider<T> _dataProvider;

    public TransportService(IDataProvider<T> provider)
    {
        _dataProvider = provider;
    }

    public async Task<List<Stop>?> GetRouteStopsAsync(int routeId, DateOnly? date = null) =>
        await new TaskFactory().StartNew(() => _dataProvider.GetStops(routeId, date));

    public List<Schedule>? GetRouteStopShedule(int routeId, DateOnly date) =>
        _dataProvider.GetSchedule(routeId, date);
    

    public async Task<List<Route>?> GetRoutesAsync() =>
        await new TaskFactory().StartNew(() => _dataProvider.GetRoutes()!);
}