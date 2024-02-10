using System.ComponentModel.DataAnnotations;
using DataSearcher.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DataSearcher.Data.Model;

public class TransportType: IModel
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