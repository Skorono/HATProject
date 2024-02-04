namespace DataSearcher.Data.Model;

public class Schedule
{
    public int StopId { get; set; }
    public int RouteId { get; set; }
    
    public DateTime ArriveDateTime { get; set; }

    public virtual Route Route { get; set; } = null!;
    public virtual Stop Stop { get; set; } = null!;
}