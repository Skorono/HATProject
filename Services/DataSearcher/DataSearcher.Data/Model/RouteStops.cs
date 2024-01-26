using System.ComponentModel.DataAnnotations.Schema;

namespace DataSearcher.Data.Model;

public partial class RouteStops 
{
    public string RouteId { get; set; }
    public string StopId { get; set; }
    
    /*public DateOnly DateOfBinding { get; set; }
    public DateOnly? DateOfUnbinding { get; set; }*/
    
    public virtual Stop Stop { get; set; }
    public virtual Route Route { get; set; }
}