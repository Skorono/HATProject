using DataSearcher.Data.Model;
using HtmlAgilityPack;

namespace DataSearcher.Domain.Helpers.Data.Parsers;

public class MosTransParser: TransportParser<HtmlDocument>
{
    public override List<Route>? ParseRoutes(HtmlDocument data)
    {
        return data.DocumentNode?
            .SelectNodes("//a[@class=\"ts-row \"]")
            .Select(
                node => new Func<Route>(delegate
                {
                    string name = node.ChildNodes
                        .First(node => node.Attributes.FirstOrDefault(attr => attr.Value == "ts-number") != null)
                        .InnerText.Trim();

                    return new Route()
                    {
                        Id = int.Parse(node.Attributes.AttributesWithName("href").First().Value.Split('/').Last()),
                        Name = name,
                        RouteTypeId = (int)_getRouteType(name),
                        TransportTypeId =
                            (int)_getTransportType(node.ChildNodes.First(node => node.Name != "i").InnerText.Trim())
                    };
                }).Invoke()
            ).ToList();
    }

    public override List<Stop>? ParseRouteStops(HtmlDocument data)
    {
        return data.DocumentNode?
            .SelectNodes("//div[@class=\"a_dotted d-inline\"]")?
            .Select( node =>
                new Stop()
                {
                    Name = node.InnerText
                }
            ).ToList();
    }

    public override List<Schedule>? ParseRouteSchedule(HtmlDocument data)
    {
        throw new NotImplementedException();
    }
}