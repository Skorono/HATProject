using System.ComponentModel.DataAnnotations;
using DataSearcher.Data.Model;

namespace DataSearcher.Domain.Helpers.Data.Parsers;

public abstract class TransportParser<T>
{
    public abstract List<Route>? ParseRoutes(T data);

    public abstract List<Stop>? ParseRouteStops(T data);

    public abstract List<Schedule>? ParseRouteSchedule(T data);

    protected RouteType.Types _getRouteType([StringLength(4)] string routeName)
    {
        return RouteType.Types.Undefined;
    }

    protected TransportType.Types _getTransportType(string transport)
    {
        return TransportType.Types.Bus;
    }
}