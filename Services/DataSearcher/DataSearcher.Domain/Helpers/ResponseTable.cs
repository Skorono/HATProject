using System.Text.Json.Nodes;

namespace DataSearcher.Domain.Helpers;

internal sealed class ResponseTable
{
    private JsonArray _table = new();

    public int HeadersCount => _table.Count;
    
    /*
    public JsonNode? FindNode(string nodeName, int depth = 10)
    {
        
    }
    */

    public void AddNode(Dictionary<string, JsonNode> node) => _table[node.Keys.First()] = node.Values.First();
    
    
}