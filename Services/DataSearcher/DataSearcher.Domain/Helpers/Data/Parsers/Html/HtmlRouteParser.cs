using HATProject.Infrastructure.Models.Transports;
using HtmlAgilityPack;

namespace DataSearcher.Domain.Helpers.Data.Parsers.Html;

public class HtmlRouteParser : ITransportParser<List<RouteDTO>, HtmlDocument>
{
    public List<RouteDTO> Parse(HtmlDocument data)
    {
        return data.DocumentNode?
            .SelectNodes("//a[@class=\"ts-row \"]")
            .Select(
                node => new Func<RouteDTO>(delegate
                {
                    var name = node.ChildNodes
                        .First(node => node.Attributes.FirstOrDefault(attr => attr.Value == "ts-number") != null)
                        .InnerText
                        .Trim();

                    return new RouteDTO
                    {
                        Id = int.Parse(node.Attributes["href"].Value.Split('/').Last()),
                        Name = name,
                        RouteType = _getRouteType(name),
                        TransportType =
                            node.SelectSingleNode(".//i[@class]").Attributes["class"].Value
                                .Trim().Split('-').LastOrDefault()!
                    };
                }).Invoke()
            ).ToList();
    }

    private string _getRouteType(string routeName)
    {
        return "";
    }

    /*private TransportType.Types _getTransportType(string nameStr)
    {
        switch (nameStr)
        {
            case "trolleybus":
                return TransportType.Types.Trolleybus;
            case "bus":
                return TransportType.Types.Bus;
            case "tramway":
                return TransportType.Types.Tramway;
            default:
                return TransportType.Types.Undefined;
        }
    }*/
}