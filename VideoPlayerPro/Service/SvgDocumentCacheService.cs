using SharpVectors.Dom.Svg;

namespace VideoPlayerPro.Service
{
    internal class SvgDocumentCacheService : CacheAbstract<string, SvgDocument>
    {
        protected override Dictionary<string, SvgDocument> Cache { get; } = [];

        public override SvgDocument Get(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("Key cannot be null or empty.", nameof(key));
            }

            if (!Cache.TryGetValue(key, out var value))
            {
                throw new KeyNotFoundException($"The key '{key}' was not found in the cache.");
            }

            return value;
        }

        public override void Set(string key, SvgDocument value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("Key cannot be null or empty.", nameof(key));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value), "Value cannot be null.");
            }

            Cache[key] = value;
        }

        
    }
}
