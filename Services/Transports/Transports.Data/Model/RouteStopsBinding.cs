using System.ComponentModel.DataAnnotations;

namespace Transports.Data.Model;

public class RouteStopsBinding
{
    [Key]
    public int BindingId { get; set; }
    public int RouteId { get; set; }
    public int StopId { get; set; }

    /*public DateOnly DateOfBinding { get; set; }
    public DateOnly? DateOfUnbinding { get; set; }*/

    public virtual Stop Stop { get; set; }
    public virtual Route Route { get; set; }
}