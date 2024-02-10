using DataSearcher.Data.Interfaces;

namespace DataSearcher.Domain.Helpers.Data;

internal sealed class ResponseTable
{
    /// <summary>
    /// responseRoute:
    ///     id:
    ///         data
    /// </summary>
    private Dictionary<string, Dictionary<string, ResponseUnit<IModel>?>> _table = new();

    public void AddResponse<T>(string header, int id, ResponseUnit<T> node) where T: class, IModel
    {
        if (!_table.ContainsKey(header))
            _table.Add(header, new Dictionary<string, ResponseUnit<IModel>?>()
            {
                { id.ToString(), node as ResponseUnit<IModel> }
            });
    }

    public List<T>? GetResponse<T>(string header, int id) where T: class, IModel
    {
        if (!(_table.ContainsKey(header) && _table[header].ContainsKey(id.ToString())))
            return null;
        
        var result = _table[header][id.ToString()];
        if (result is { IsOutdated: true })
        {
            _table[header][id.ToString()] = null;
        }

        return result?.Data.Select(d => d as T)?.ToList()!;
    }
        
}