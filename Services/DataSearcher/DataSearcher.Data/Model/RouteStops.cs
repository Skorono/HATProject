using System.ComponentModel.DataAnnotations.Schema;

namespace DataSearcher.Data.Model;

public class RouteStops 
{
    
    
    public string RouteId { get; set; }
    public string StopId { get; set; }
    
    public DateOnly DateOfBinding { get; set; }
    public DateOnly? DateOfUnbinding { get; set; }
    
    public Stop Stop { get; set; }
    public Route Route { get; set; }
}