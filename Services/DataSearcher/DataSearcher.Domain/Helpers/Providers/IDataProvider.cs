using System.Text.Json.Nodes;

namespace DataSearcher.Domain.Helpers.Providers;

public interface IDataProvider
{
    public JsonObject GetRoutes();
    
    public JsonObject GetStops(string routeId);
    
    public JsonObject GetSchedule(string routeId, string stopName, DateOnly? date = null);
}