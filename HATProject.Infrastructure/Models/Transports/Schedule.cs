namespace HATProject.Infrastructure.Models.Transports;

public class Schedule
{
    public RouteDTO Route { get; set; }
    public StopDTO Stop { get; set; }
    public DateTime ArriveDateTime { get; set; }
}