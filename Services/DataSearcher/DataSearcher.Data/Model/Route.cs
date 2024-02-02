using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataSearcher.Data.Model;

public partial class Route
{
    [Column(TypeName = "VARCHAR")]
    [StringLength(4)]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int TransportTypeId { get; set; }
    public int RouteTypeId { get; set; }
    
    public virtual TransportType TransportType { get; set; } = null!;
    public virtual RouteType RouteType { get; set; } = null!;
}