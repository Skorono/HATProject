namespace DataSearcher.Data.Model;

public partial class RouteType
{
    public Types Id { get; set; }
    public string Name { get; set; } = null!;
    
    public enum Types
    {
        Mainline = 1,
        Local,
        Social,
        Diametrical,
        Express
    }
}