using DataSearcher.Data.Context;
using DataSearcher.Data.Model;
using DataSearcher.Domain.Helpers.Data.Providers;
using Microsoft.Extensions.Logging;

namespace DataSearcher.Domain.Services;

public class TransportService<T>
{
    private readonly IDataProvider<T> _dataProvider;
    private readonly TransportRouteContext _context;
    private readonly CacheManager _cacheManager;
    private readonly ILogger<TransportService<T>> _logger;
    
    public TransportService(IDataProvider<T> provider, TransportRouteContext context, 
        CacheManager cache, ILogger<TransportService<T>> logger)
    {
        _dataProvider = provider;
        _logger = logger;
        _context = context;
        _cacheManager = cache;
    }

    public async Task<List<Stop>?> GetRouteStopsAsync(int routeId, DateOnly? date = null) =>
        await new TaskFactory().StartNew(() => _dataProvider.GetStops(routeId, date));

    public List<Schedule>? GetRouteStopShedule(int routeId, DateOnly date) =>
        _dataProvider.GetSchedule(routeId, date);
    
    public async Task<List<List<Route>?>> GetRoutesPagesAsync() =>
        await new TaskFactory().StartNew(() => _dataProvider.GetAllRoutesPages()!);
}