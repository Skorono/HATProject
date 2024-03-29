namespace Transports.Data.Model;

public class RouteType
{
    public enum Types
    {
        Mainline = 1,
        Local,
        Social,
        Diametrical,
        Express,
        Undefined
    }

    public int Id { get; set; }
    public string Name { get; set; } = null!;
}