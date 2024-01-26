using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataSearcher.Data.Model;

public partial class Route
{
    [Column(TypeName = "VARCHAR")]
    [StringLength(4)]
    public string Id { get; set; } = null!;
    public virtual RouteType Type { get; set; } = null!;
}