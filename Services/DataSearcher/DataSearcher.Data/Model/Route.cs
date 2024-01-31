using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataSearcher.Data.Model;

public partial class Route
{
    [Column(TypeName = "VARCHAR")]
    [StringLength(4)]
    public int Id;

    public string Name { get; set; } = null!;

    public int TransportTypeId;
    public int RouteTypeId;
    
    public virtual TransportType TransportType { get; set; } = null!;
    public virtual RouteType RouteType { get; set; } = null!;
}