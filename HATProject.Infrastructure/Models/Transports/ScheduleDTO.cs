namespace HATProject.Infrastructure.Models.Transports;

public class ScheduleDTO
{
    public int RouteId { get; set; }
    public int StopId { get; set; }
    public DateTime ArriveDateTime { get; set; }
}