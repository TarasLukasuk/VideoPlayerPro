namespace VideoPlayerPro.Service;

internal class SvgDocumentCacheService : CacheAbstract<string, SvgDocument>
{
    private readonly object _lock = new();

    protected override Dictionary<string, SvgDocument> Cache { get; } = new Dictionary<string, SvgDocument>();

    public override SvgDocument Get(string key)
    {
        key = NormalizeCacheKey(key);

        lock (_lock)
        {
            if (!Cache.TryGetValue(key, out var value))
            {
                throw new KeyNotFoundException($"The key '{key}' was not found in the cache.");
            }

            return value;
        }
    }

    public override void Set(string key, SvgDocument value)
    {
        key = NormalizeCacheKey(key);

        ArgumentNullException.ThrowIfNull(value);

        lock (_lock)
        {
            Cache[key] = value;
        }
    }

    private static string NormalizeCacheKey(string key)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(key);

        return key.Trim().ToLowerInvariant();
    }
}
