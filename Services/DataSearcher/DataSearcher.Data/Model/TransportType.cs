namespace DataSearcher.Data.Model;

public class TransportType
{
    public enum Types
    {
        Bus = 1,
        Tramway,
        Trolleybus
    }

    public Types Id;
    public string Name { get; set; } = null!;
}