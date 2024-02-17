using DataSearcher.Data.Model;
using DataSearcher.Domain.Helpers.Data.Parsers;

namespace DataSearcher.Domain.Helpers.Data.Providers;

public interface IDataProvider<TInput>
{
    public List<List<Route>?>? GetAllRoutesPages(ITransportParser<List<Route>, TInput>? parser = null);

    public List<Stop>? GetStops(int routeId, DateOnly? date = null, 
        ITransportParser<List<Stop>, TInput>? parser = null);

    public List<Schedule>? GetSchedule(int routeId, DateOnly? date = null,
        ITransportParser<List<Schedule>, TInput>? parser = null);
}