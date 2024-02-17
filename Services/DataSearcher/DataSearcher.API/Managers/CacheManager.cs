using System.Text.Json;
using DataSearcher.Data.Others;
using Microsoft.Extensions.Caching.Distributed;

namespace DataSearcher.Api.Managers;

public sealed class CacheManager(IDistributedCache cache)
{
    public List<CacheTitle> Keys { get; } = new();

    public async void AddResponse<T>(CacheTitle title, ResponseUnit<T> node) where T : class
    {
        if (!Keys.Contains(title))
            Keys.Add(title);
        await cache.SetStringAsync(title.ToString(), JsonSerializer.Serialize(node));
    }

    public async Task<ResponseUnit<T>?> GetResponse<T>(CacheTitle title) where T : class
    {
        string? cachedString = await cache.GetStringAsync(title.ToString());

        if (cachedString == null)
            return null;

        if (!Keys.Any(key => key.ToString() == title.ToString()))
            Keys.Add(title);

        var cacheResult = JsonSerializer.Deserialize<ResponseUnit<T>>(cachedString);
        if (cacheResult is { IsOutdated: false })
            return cacheResult;
        return null;
    }

    public List<ResponseUnit<T>>? GetResponsesByHeader<T>(string header) where T : class
    {
        return Keys.Where(key => key.Header == header)?
            .Select(async key => await GetResponse<T>(key))?
            .Select(t => t.Result!).ToList();
    }

    public class CacheTitle(string header, int id)
    {
        public string Header { get; } = header;
        public int Id { get; } = id;

        public override string ToString()
        {
            return $"{Header}:{Id}";
        }
    }
}