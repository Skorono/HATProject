namespace Transports.Data.Model;

public class Schedule
{
    public int BindingId { get; set;  }
    public DateTime ArriveDateTime { get; set; }

    public virtual RouteStopsBinding Binding { get; set; } = null!;
}