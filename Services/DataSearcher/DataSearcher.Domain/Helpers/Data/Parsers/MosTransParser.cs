using DataSearcher.Data.Model;
using HtmlAgilityPack;

namespace DataSearcher.Domain.Helpers.Data.Parsers;

public class MosTransParser: ITransportParser<HtmlDocument>
{
    public List<Route>? ParseRoutes(HtmlDocument data)
    {
        // now it too slow, but that`s the plan!
        /// Todo fix this anyway
        return data.DocumentNode?
            .SelectNodes("//a[@class=\"ts-row \"]")
            .Select(
                node => new Route()
                {
                    Id = int.Parse(node.Attributes.AttributesWithName("href").First().Value.Split('/').Last()),
                    Name = node.ChildNodes.First(node => node.Attributes.FirstOrDefault(attr => attr.Value == "ts-number") != null).InnerText.Trim(),
                    RouteTypeId = (int)RouteType.Types.Local,
                    TransportTypeId = (int)TransportType.Types.Bus
                }
            ).ToList();
    }

    public List<Stop>? ParseRouteStops(HtmlDocument data)
    {
        throw new NotImplementedException();
    }

    public List<Schedule>? ParseRouteSchedule(HtmlDocument data)
    {
        throw new NotImplementedException();
    }
}