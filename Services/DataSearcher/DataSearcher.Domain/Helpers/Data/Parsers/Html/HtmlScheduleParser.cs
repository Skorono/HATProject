using HATProject.Infrastructure.Models.Transports;
using HtmlAgilityPack;

namespace DataSearcher.Domain.Helpers.Data.Parsers.Html;

public class HtmlScheduleParser : ITransportParser<List<ScheduleDTO>, HtmlDocument>
{
    public List<ScheduleDTO> Parse(HtmlDocument data)
    {
        var stops = new HtmlStopsParser().Parse(data);
        List<ScheduleDTO> schedules = new();

        var stopNumber = 0;
        foreach (var stopSchedule in data.DocumentNode?.SelectNodes("//div[@class=\"raspisanie_hover\"]"))
        {
            foreach (var node in stopSchedule.SelectNodes(".//div[@class=\"raspisanie_data \"]")!)
            {
                var stop = stops.ElementAt(stopNumber);

                var routeId = int.Parse(stopSchedule.SelectSingleNode("//li[@data-route]").Attributes
                    .AttributesWithName("data-route").First().Value!);

                var hour = node.SelectSingleNode(".//div[@class=\"dt1\"]").InnerText;

                foreach (var minuteNode in node.SelectNodes(".//div[@class=\"div10\"]"))
                    schedules.Add(new ScheduleDTO
                    {
                        RouteId = routeId,
                        StopId = stop.Id,
                        ArriveDateTime = DateTime.Parse(hour.Trim() + minuteNode.InnerText.Trim())
                    });
            }

            stopNumber++;
        }

        return schedules;
    }
}