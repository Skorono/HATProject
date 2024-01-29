using DataSearcher.Data.Model;

namespace DataSearcher.Domain.Services;

public interface ITransportParser
{
    public string GetRouteStops(string routeId);
    public string GetRouteStopShedule(string routeId, string stopName);
    public IEnumerable<string> GetRoutes();
}