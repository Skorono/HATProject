using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Transports.Data.Model;

public class TransportType
{
    public enum Types
    {
        Bus = 1,
        Tramway,
        Trolleybus,
        Undefined
    }
    
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}