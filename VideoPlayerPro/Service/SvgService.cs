using SharpVectors.Converters;
using SharpVectors.Dom.Svg;
using SharpVectors.Renderers.Wpf;
using System.IO;
using System.Windows.Media;
using VideoPlayerPro.Utilities;

namespace VideoPlayerPro.Service;

internal sealed class SvgService : SvgAbstract
{
    private static readonly string _svgFolderPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Resources", "Icons"));
    private static readonly SvgDocumentCacheService _svgDocumentCacheService = new();

    public HashSet<string> KnownIcons { get; } 

    private static readonly WpfDrawingSettings _settings = new()
    {
        IncludeRuntime = false,
        TextAsGeometry = true,
        OptimizePath = true
    };

    public SvgService()
    {
        KnownIcons = Enum.GetNames<IconType>().ToHashSet(StringComparer.OrdinalIgnoreCase);
    }

    public override DrawingImage Load(string svgName)
    {
        string path = Path.Combine(_svgFolderPath, svgName + ".svg");

        if (!File.Exists(path))
        {
            throw new FileNotFoundException("SVG file not found.", path);
        }

        using FileSvgReader reader = new(_settings);
        {
            DrawingGroup drawing = reader.Read(path);
            SvgDocument? svgCopy = SvgCopier.Copy(reader.SvgDocument);

            _svgDocumentCacheService.Set(svgName, svgCopy);

            return DrawingImageConverter.Convert(drawing);
        }
    }

    public override DrawingImage ChangeColor(SvgDocument svgDocument, string oldColor, string newColor)
    {
        throw new NotImplementedException();
    }
}