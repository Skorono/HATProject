using DataSearcher.Data.Model;
using HtmlAgilityPack;

namespace DataSearcher.Domain.Helpers.Data.Parsers;

/// <summary>
///  html --> json --> Model
///  
/// </summary>
public interface ITransportParser<T>
{
    public List<Route>? ParseRoutes(T data);

    public List<Stop>? ParseRouteStops(T data);
    
    public List<Schedule>? ParseRouteSchedule(T data);
}