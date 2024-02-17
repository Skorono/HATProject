using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Transports.Data.Model;

public class Route
{
    [Column(TypeName = "VARCHAR")]
    [StringLength(4)]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int TransportTypeId { get; set; }
    
    public int RouteTypeId { get; set; }

    [ForeignKey("TransportTypeId")]
    public virtual TransportType TransportType { get; set; } = null!;
    
    [ForeignKey("RouteTypeId")]
    public virtual RouteType RouteType { get; set; } = null!;
}