namespace VideoPlayerPro.Utilities;

class SvgNameNormalizer
{
    public static string Normalize(string name, string targetChar, string replacementChar)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(targetChar);
        ArgumentException.ThrowIfNullOrWhiteSpace(replacementChar);

        name = Path.GetFileName(name);

        if (name.EndsWith(".svg", StringComparison.OrdinalIgnoreCase))
        {
            name = name[..^4];
        }

        return name.Replace(targetChar, replacementChar).ToLowerInvariant();
    }

    public static string Normalize(string key)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(key);

        if (key.EndsWith(".svg", StringComparison.OrdinalIgnoreCase))
        {
            key = key[..^4];
        }

        return key.ToLowerInvariant();
    }
}
