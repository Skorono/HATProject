using DataSearcher.Data.Model;

namespace DataSearcher.Domain.Services;

public interface ITransportParser
{
    public RouteStops GetRouteStops(string routeId);

    public Schedule GetRouteStopShedule(Route route, Stop busStop);
}