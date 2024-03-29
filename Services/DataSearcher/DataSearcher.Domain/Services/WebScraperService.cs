using System.Net;
using DataSearcher.Domain.Helpers.Data.Parsers;
using DataSearcher.Domain.Helpers.Data.Parsers.Html;
using HATProject.Infrastructure.Models.Transports;
using HtmlAgilityPack;

namespace DataSearcher.Domain.Services;

public class WebScraperService : IDataProvider<HtmlDocument>
{
    public static readonly string BaseUrl = "https://transport.mos.ru/";

    // don`t forget to delete :D
    public static readonly string UserAgent =
        "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/121.0.0.0 Safari/537.36";

    private readonly HttpClient _client = new() { BaseAddress = new Uri(BaseUrl) };

    public WebScraperService()
    {
        _client.Timeout = TimeSpan.FromMinutes(30);
    }

    public List<List<RouteDTO>?>? GetAllRoutesPages(ITransportParser<List<RouteDTO>, HtmlDocument>? parser = null)
    {
        parser = parser ?? new HtmlRouteParser();

        var page = 1;
        List<Task<List<RouteDTO>?>> tasks = new();
        while (tasks.Count <= 0 || (tasks.Last().Result != null && tasks.Last().Status == TaskStatus.RanToCompletion))
            tasks.Add(
                Task
                    .Run(() => GetRoutePage(page++))
                    .ContinueWith(task => task.Result == null ? null : parser.Parse(task.Result))
            );

        return tasks.Select(task => task.Result).ToList();
    }

    public List<StopDTO>? GetStops(int routeId, DateOnly? date = null,
        ITransportParser<List<StopDTO>, HtmlDocument>? parser = null)
    {
        parser = parser ?? new HtmlStopsParser();

        date = date ?? DateOnly.Parse(DateTime.Today.ToShortDateString());

        var schedulePage = _getHtmlDoc("ru/ajax/App/ScheduleController/getRoute?" +
                                       "mgt_schedule[BisNight]=&" +
                                       $"mgt_schedule[date]={date}&" +
                                       $"mgt_schedule[route]={routeId}&" +
                                       "mgt_schedule[direction]=0",
            new Dictionary<string, string>
            {
                { "User-Agent", UserAgent },
                { "X-Requested-With", "XMLHttpRequest" }
            });

        return schedulePage != null ? parser.Parse(schedulePage) : null;
    }

    public List<ScheduleDTO>? GetSchedule(int routeId, DateOnly? date = null,
        ITransportParser<List<ScheduleDTO>?, HtmlDocument>? parser = null)
    {
        parser = parser ?? new HtmlScheduleParser()!;

        date = date ?? DateOnly.Parse(DateTime.Today.ToShortDateString());

        var schedulePage = _getHtmlDoc("ru/ajax/App/ScheduleController/getRoute?" +
                                       "mgt_schedule[BisNight]=&" +
                                       $"mgt_schedule[date]={date}&" +
                                       $"mgt_schedule[route]={routeId}&" +
                                       "mgt_schedule[direction]=0",
            new Dictionary<string, string>
            {
                { "User-Agent", UserAgent },
                { "X-Requested-With", "XMLHttpRequest" }
            });

        return schedulePage != null ? parser.Parse(schedulePage) : null;
    }

    public HtmlDocument? GetRoutePage(int page)
    {
        var responseUrl = $"ru/ajax/App/ScheduleController/getRoutesList?" +
                          $"mgt_schedule[search]=&" +
                          $"mgt_schedule[isNight]=&" +
                          $"mgt_schedule[workTime]=1&" +
                          $"mgt_schedule[direction]=0&" +
                          $"page={page}";

        return _getHtmlDoc(responseUrl, new Dictionary<string, string>
        {
            { "User-Agent", UserAgent },
            { "X-Requested-With", "XMLHttpRequest" }
        });
    }

    private HtmlDocument? _getHtmlDoc(string responseUri, Dictionary<string, string>? clientParams = null)
    {
        HtmlDocument doc = new();

        _client.DefaultRequestHeaders.Clear();
        if (clientParams != null)
            foreach (var param in clientParams)
                _client.DefaultRequestHeaders.Add(param.Key, param.Value);

        try
        {
            SemaphoreSlim semaphore = new(100);
            var request = _client.GetAsync(responseUri).Result;
            semaphore.Release();

            if (request?.StatusCode == HttpStatusCode.OK)
                doc.LoadHtml(request.Content.ReadAsStringAsync().Result);
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e);
        }

        return doc.ParsedText != null ? doc : null;
    }
}