using DataSearcher.Data.Model;

namespace DataSearcher.Domain.Helpers.Data.Providers;

public interface IDataProvider
{
    public List<Route>? GetRoutes();

    public List<Stop>? GetStops(int routeId, DateOnly? date = null);

    public Dictionary<string, List<Schedule>?> GetSchedule(int routeId, DateOnly? date = null);
}