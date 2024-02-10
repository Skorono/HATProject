using IModel = DataSearcher.Data.Interfaces.IModel;

namespace DataSearcher.Data.Model;

public class Stop: IModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}