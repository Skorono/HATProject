using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata;
using IModel = DataSearcher.Data.Interfaces.IModel;

namespace DataSearcher.Data.Model;

public class Route: IModel
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