using System.Net;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Unicode;
using HtmlAgilityPack;

namespace DataSearcher.Domain.Helpers.Providers;

internal class WebScraper: IDataProvider
{
    public static readonly string BaseUrl = "https://transport.mos.ru/";

    private ResponseTable _table = new();
    private HttpClient _client = new() { BaseAddress = new Uri(BaseUrl) };
    
    public WebScraper()
    {
        _client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/121.0.0.0 Safari/537.36");
    }

    private HtmlDocument? _getRoutePage(int page = 1)
    {
        string responseUrl = $"ru/ajax/App/ScheduleController/getRoutesList?" +
                             $"mgt_schedule%5Bsearch%5D=&mgt_schedule%5BisNight%5D=&mgt_schedule" +
                             $"%5BworkTime%5D=1&mgt_schedule%5Bdirection%5D=0&page={page}";
        return _getHtmlDoc(responseUrl);
    }
    
    private HtmlDocument? _getHtmlDoc(string responseUri)
    {
        HtmlDocument doc = new();
        var request = _client.GetAsync(responseUri).Result; 
        doc.LoadHtml(request.Content.ReadAsStringAsync().Result);

        return request.StatusCode == HttpStatusCode.OK ? doc : null;
    }

    public JsonObject GetRoutes()
    {
        JsonObject json = new();
        HtmlDocument? doc = _getRoutePage(2);

        json["routes"] = JsonSerializer.Serialize(
            doc.DocumentNode?
                .SelectNodes("//div[@class=\"ts-number\"]")
                .Select(node => node.InnerText.Trim()),
            
            new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            });
        
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