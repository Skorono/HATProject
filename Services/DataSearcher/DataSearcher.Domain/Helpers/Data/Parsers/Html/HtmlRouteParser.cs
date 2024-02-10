using DataSearcher.Data.Model;
using HtmlAgilityPack;

namespace DataSearcher.Domain.Helpers.Data.Parsers.Html;

public class HtmlRouteParser: ITransportParser<List<Route>, HtmlDocument>
{
    public List<Route> Parse(HtmlDocument data)
    {
        return data.DocumentNode?
            .SelectNodes("//a[@class=\"ts-row \"]")
            .Select(
                node => new Func<Route>(delegate
                {
                    var name = node.ChildNodes
                        .First(node => node.Attributes.FirstOrDefault(attr => attr.Value == "ts-number") != null)
                        .InnerText
                        .Trim();

                    return new Route
                    {
                        Id = int.Parse(node.Attributes["href"].Value.Split('/').Last()),
                        Name = name,
                        RouteTypeId = (int)_getRouteType(name),
                        TransportTypeId =
                            (int)_getTransportType(node.SelectSingleNode(".//i[@class]").Attributes["class"].Value.Trim())
                    };
                }).Invoke()
            ).ToList();        
    }

    private RouteType.Types _getRouteType(string routeName)
    {
        return RouteType.Types.Local;
    }

    private TransportType.Types _getTransportType(string nameStr)
    {
        switch (nameStr.Split('-').LastOrDefault())
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
    }
}