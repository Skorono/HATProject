namespace DataSearcher.Data.Model;

public class TransportType
{
    public Types Id;
    public string Name { get; set; } = null!;
    
    public enum Types
    {
        Bus = 1,
        Tramway,
        Trolleybus
    }
}