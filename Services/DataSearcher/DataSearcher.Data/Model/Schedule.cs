namespace DataSearcher.Data.Model;

public partial class Schedule
{
    public int RouteId { get; set; }
    
    public virtual Route Route { get; set; } = null!;
}