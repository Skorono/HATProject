using DataSearcher.Data.Interfaces;

namespace DataSearcher.Data.Model;

public class Schedule: IModel
{
    public int BindingId { get; set;  }
    public DateTime ArriveDateTime { get; set; }

    public virtual RouteStopsBinding Binding { get; set; } = null!;
}