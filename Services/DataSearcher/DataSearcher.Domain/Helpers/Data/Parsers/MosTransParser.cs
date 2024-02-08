using DataSearcher.Data.Model;
using HtmlAgilityPack;

namespace DataSearcher.Domain.Helpers.Data.Parsers;

public class MosTransParser : TransportParser<HtmlDocument>
{
    public override List<Route>? ParseRoutes(HtmlDocument data)
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
            .Select(node =>
                new Stop
                {
                    Name = node.InnerText.Replace("&quot;", "'")
                }
            ).ToList();
    }

    public override Dictionary<string, List<Schedule>?> ParseRouteSchedule(HtmlDocument data)
    {
        Dictionary<string, List<Schedule?>> schedules = new();
        foreach (var enumerable in ParseRouteStops(data))
        { 
            schedules[enumerable.Name] = new();
        } 

        int stopNumber = 0;
        foreach (var stopSchedule in data.DocumentNode?.SelectNodes("//div[@class=\"raspisanie_hover\"]"))
        {
            foreach (var node in stopSchedule.SelectNodes(".//div[@class=\"raspisanie_data \"]")!)
            {
                var schedule = schedules.ElementAt(stopNumber).Value;

                var hour = node.SelectSingleNode(".//div[@class=\"dt1\"]").InnerText;

                foreach (var minuteNode in node.SelectNodes(".//div[@class=\"div10\"]"))
                {

                    schedule.Add(new Schedule()
                    {
                        ArriveDateTime = DateTime.Parse($"{hour.Trim()}{minuteNode.InnerText.Trim()}")
                    });
                }

            }
            stopNumber++;
        }

        return schedules;

        /*return schedules.Keys.Count < 0 ? null
            : data.DocumentNode?
                .SelectNodes("//div[@class=\"raspisanie_hover\"]")
                .Select(node =>
                    node.ChildNodes
                        .Select(el =>
                            el.Attributes.AttributesWithName("class").First(attr => attr.Value == "raspisanie_data ").OwnerNode)
                        .Select(node => node.SelectNodes("//div[@class=\"raspisanie_data2\"]/div").Select(dateNode => schedules))
                );*/
    }
}