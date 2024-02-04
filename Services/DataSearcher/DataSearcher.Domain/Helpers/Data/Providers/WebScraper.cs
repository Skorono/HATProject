using System.Net;
using DataSearcher.Data.Model;
using DataSearcher.Domain.Helpers.Data.Parsers;
using HtmlAgilityPack;

namespace DataSearcher.Domain.Helpers.Data.Providers;

internal class WebScraper : IDataProvider
{
    public static readonly string BaseUrl = "https://transport.mos.ru/";

    // don`t forget to delete :D
    public static readonly string UserAgent =
        "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/121.0.0.0 Safari/537.36";

    private readonly HttpClient _client = new() { BaseAddress = new Uri(BaseUrl) };
    private readonly TransportParser<HtmlDocument> _parser;
    private ResponseTable _table = new();

    public WebScraper(TransportParser<HtmlDocument>? parser = null)
    {
        if (parser != null)
            _parser = parser;
        else
            _parser = new MosTransParser();
        _client.Timeout = TimeSpan.FromMinutes(30);
    }

    public List<Route>? GetRoutes()
    {
        var page = 1;
        List<Task<List<Route>?>> tasks = new();
        while (tasks.Count <= 0 || (tasks.Last().Result != null && tasks.Last().Status == TaskStatus.RanToCompletion))
            tasks.Add(
                Task
                    .Run(() => GetRoutePage(page++))
                    .ContinueWith(task => task.Result == null ? null : _parser.ParseRoutes(task.Result))
            );

        return tasks.Where(task => task.Result != null)
            .SelectMany(task => task.Result!).ToList();
    }

    public List<Stop>? GetStops(int routeId, DateOnly? date = null)
    {
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

        return schedulePage != null ? _parser.ParseRouteStops(schedulePage) : null;
    }

    public Schedule? GetSchedule(string routeId, string stopName, DateOnly? date = null)
    {
        throw new NotImplementedException();
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