using SharpVectors.Dom.Svg;

namespace VideoPlayerPro.Service
{
    internal abstract class CacheAbstract<TKey, TValue>
    {
        protected abstract Dictionary<TKey, TValue> Cache { get; }

        public abstract TValue Get(TKey key);
        public abstract void Set(TKey key, TValue value);
    }
}
