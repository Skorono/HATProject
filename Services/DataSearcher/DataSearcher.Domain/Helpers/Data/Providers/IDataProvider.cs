using System.Text.Json.Nodes;
using DataSearcher.Data.Model;

namespace DataSearcher.Domain.Helpers.Data.Providers;

public interface IDataProvider
{
    public List<Route>? GetRoutes();
    
    public List<Stop>? GetStops(int routeId, DateOnly? date = null);
    
    public Schedule? GetSchedule(string routeId, string stopName, DateOnly? date = null);
}