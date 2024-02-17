using DataSearcher.Domain.Helpers.Data.Parsers;
using HATProject.Infrastructure.Models.Transports;

namespace DataSearcher.Domain.Services;

public interface IDataProvider<TInput>
{
    public List<List<RouteDTO>?>? GetAllRoutesPages(ITransportParser<List<RouteDTO>, TInput>? parser = null);

    public List<StopDTO>? GetStops(int routeId, DateOnly? date = null,
        ITransportParser<List<StopDTO>, TInput>? parser = null);

    public List<ScheduleDTO>? GetSchedule(int routeId, DateOnly? date = null,
        ITransportParser<List<ScheduleDTO>, TInput>? parser = null);
}