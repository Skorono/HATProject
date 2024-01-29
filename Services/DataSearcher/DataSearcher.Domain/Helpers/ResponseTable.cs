using System.Text.Json.Nodes;

namespace DataSearcher.Domain.Helpers;

public sealed class ResponseTable
{
    private JsonObject _table = new();    
    
    public JsonObject Table { get; set; }
    
    /*
     not implemented yet
     dumps table to one of format like xml or xlsx
     */
    public void Dump() => throw new NotImplementedException();
}