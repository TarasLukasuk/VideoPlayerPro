namespace VideoPlayerPro.Utilities
{
    internal static class SvgCopier
    {
        public static SvgDocument Copy(SvgDocument source)
        {
            ArgumentNullException.ThrowIfNull(source);

            if (source.Window is not SvgWindow window)
            {
                throw new InvalidOperationException($"Unsupported SVG window type: {source.Window?.GetType().Name ?? "null"}.");
            }

            var copy = new SvgDocument(window);
            copy.LoadXml(source.OuterXml);
            return copy;
        }
    }
}