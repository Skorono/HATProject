using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using HATProject.Infrastructure.Models.Transports;
using HtmlAgilityPack;

namespace DataSearcher.Domain.Helpers.Data.Parsers.Html;

public class HtmlStopsParser : ITransportParser<List<StopDTO>, HtmlDocument>
{
    public List<StopDTO>? Parse(HtmlDocument data)
    {
        List<StopDTO> stops = new();

        var dataJson = JsonSerializer.Deserialize<JsonObject>(
            data.DocumentNode?
                .SelectSingleNode("//div[@class=\"schedule-route clearfix schedule-route--ru\"]")
                .Attributes.First(attr => attr.Name == "data-coords").Value
                .Replace("&quot;", new StringBuilder().Append('"').ToString())!
        );

        foreach (var stopInf in dataJson["features"].AsArray().SkipLast(1))
            stops.Add(new StopDTO
            {
                Id = stopInf["id"].GetValue<int>(),
                Name = stopInf["properties"].AsObject()["hintContent"].ToString()
            });

        return stops;
    }
}