using DataSearcher.Data.Model;
using HtmlAgilityPack;

namespace DataSearcher.Domain.Helpers.Data.Parsers.Html;

public class HtmlScheduleParser: ITransportParser<List<Schedule>, HtmlDocument>
{
    public List<Schedule> Parse(HtmlDocument data)
    {
        List<Stop> stops = new HtmlStopsParser().Parse(data);
        List<Schedule> schedules = new();

        int stopNumber = 0;
        foreach (var stopSchedule in data.DocumentNode?.SelectNodes("//div[@class=\"raspisanie_hover\"]"))
        {
            foreach (var node in stopSchedule.SelectNodes(".//div[@class=\"raspisanie_data \"]")!)
            {
                var stop = stops.ElementAt(stopNumber);
                
                int routeId = int.Parse(stopSchedule.SelectSingleNode("//li[@data-route]").Attributes
                    .AttributesWithName("data-route").First().Value!);

                var hour = node.SelectSingleNode(".//div[@class=\"dt1\"]").InnerText;

                foreach (var minuteNode in node.SelectNodes(".//div[@class=\"div10\"]"))
                {
                    schedules.Add(new Schedule()
                    {
                        Binding = new RouteStopsBinding()
                        {
                            RouteId = routeId,
                            StopId = stop.Id 
                        },
                        ArriveDateTime = DateTime.Parse(hour.Trim() + minuteNode.InnerText.Trim())
                    });
                }
            }
            stopNumber++;
        }

        return schedules;
    }
}