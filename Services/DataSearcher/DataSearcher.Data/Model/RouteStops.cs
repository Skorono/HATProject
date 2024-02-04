namespace DataSearcher.Data.Model;

public class RouteStops
{
    public string RouteId { get; set; }
    public string StopId { get; set; }

    /*public DateOnly DateOfBinding { get; set; }
    public DateOnly? DateOfUnbinding { get; set; }*/

    public virtual Stop Stop { get; set; }
    public virtual Route Route { get; set; }
}