using System.Net;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Unicode;
using DataSearcher.Data.Model;
using DataSearcher.Domain.Helpers.Data.Parsers;
using HtmlAgilityPack;

namespace DataSearcher.Domain.Helpers.Data.Providers;

internal class WebScraper: IDataProvider
{
    public static readonly string BaseUrl = "https://transport.mos.ru/";

    // don`t forget to delete :D
    public static readonly string UserAgent =
        "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/121.0.0.0 Safari/537.36";
    
    private TransportParser<HtmlDocument> _parser;
    private ResponseTable _table = new();
    
    public WebScraper(TransportParser<HtmlDocument>? parser = null)
    {
        if (parser != null)
            _parser = parser;
        else
            _parser = new MosTransParser();
    }
    
    public HtmlDocument? GetRoutePage(int page)
    {
        string responseUrl = $"ru/ajax/App/ScheduleController/getRoutesList?" +
                             $"mgt_schedule[search]=&" +
                             $"mgt_schedule[isNight]=&" +
                             $"mgt_schedule[workTime]=1&" +
                             $"mgt_schedule[direction]=0&" +
                             $"page={page}";
        
        return _getHtmlDoc(responseUrl, new Dictionary<string, string>
        {
            {"User-Agent", UserAgent},
            {"X-Requested-With", "XMLHttpRequest"}
        });
    }
    
    private HtmlDocument? _getHtmlDoc(string responseUri, Dictionary<string, string>? clientParams = null)
    {
        HtmlDocument doc = new();
        using (var client = new HttpClient() { BaseAddress = new Uri(BaseUrl) })
        {
            foreach (var param in clientParams)
                client.DefaultRequestHeaders.Add(param.Key, param.Value);

            var request = client.GetAsync(responseUri).Result;
            if (request.StatusCode == HttpStatusCode.OK)
                doc.LoadHtml(request.Content.ReadAsStringAsync().Result);
        }

        return doc.ParsedText != null ? doc : null;
    }

    public JsonObject GetRoutes()
    {
        JsonObject json = new();

        int page = 1;
        HtmlDocument? routePage = new();
        List<List<Route>> routes = new();
        while (routePage != null)
        {
            routePage = GetRoutePage(page);
            if (routePage != null) routes.Add(_parser.ParseRoutes(routePage)!);
            
            page++;
        }
        
        return json;
    }

    public JsonObject GetStops(string routeId)
    {
        throw new NotImplementedException();
    }

    public JsonObject GetSchedule(string routeId, string stopName, DateOnly? date = null)
    {
        throw new NotImplementedException();
    }
}