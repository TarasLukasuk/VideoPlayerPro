using System.Windows.Media;
using VideoPlayerPro.Utilities;
using VideoPlayerPro.ViewModels.Base;

namespace VideoPlayerPro.Service;

public class IconsProviderService : ViewModelBase
{
    private static SvgService _svgService =new();

    public DrawingImage this[string index, string targetChar, string replacementChar]
    {
        get
        {
            if (!_svgService.KnownIcons.Contains(index))
            {
                throw new ArgumentException($"SVG name '{index}' does not match any IconType enum value.", nameof(index));
            }

            return _svgService.Load(SvgNameNormalizer.Normalize(index, targetChar, replacementChar));
        }
    }

    public DrawingImage this[string index]
    {
        get
        {
            if (!_svgService.KnownIcons.Contains(index))
            {
                throw new ArgumentException($"SVG name '{index}' does not match any IconType enum value.", nameof(index));
            }

            return _svgService.Load(SvgNameNormalizer.Normalize(index));
        }
    }
}
