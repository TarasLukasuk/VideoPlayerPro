using SharpVectors.Converters;
using SharpVectors.Dom.Svg;
using SharpVectors.Renderers.Wpf;
using System.IO;
using System.Windows.Media;
using VideoPlayerPro.Converters;
using VideoPlayerPro.Enums;

namespace VideoPlayerPro.Service
{
    internal class SvgService : SvgAbstract
    {
        private static readonly string SvgFolderPath = Path.Combine(AppContext.BaseDirectory, "Resources", "Icons");

        private static WpfDrawingSettings CreateSettings()
        {
            WpfDrawingSettings wpfDrawingSettings = new()
            {
                IncludeRuntime = false,
                TextAsGeometry = true,
                OptimizePath = true
            };

            return wpfDrawingSettings;
        }

        public override DrawingImage Load(string svgName)
        {
            if (string.IsNullOrWhiteSpace(svgName))
            {
                throw new ArgumentException("SVG name cannot be null or empty.", nameof(svgName));
            }

            string fileName = Path.GetFileName(svgName);
            string iconName = Path.GetFileNameWithoutExtension(fileName);

            if (!Enum.GetNames<IconType>().Contains(iconName, StringComparer.OrdinalIgnoreCase))
            {
                throw new ArgumentException($"'{svgName}' is not a known icon.", nameof(svgName));
            }

            string path = Path.Combine(SvgFolderPath, Path.ChangeExtension(fileName, ".svg"));

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("SVG file not found.", path);
            }

            using FileSvgReader reader = new(CreateSettings());

            DrawingGroup drawing = reader.Read(path);

            SvgDocument? svgCopy = SvgCopier.Copy(reader.SvgDocument) ?? throw new InvalidOperationException("Failed to copy SVG document.");

            CacheAbstract<string, SvgDocument> svgCacheService = new SvgDocumentCacheService();
            svgCacheService.Set(iconName, svgCopy);

            return DrawingImageConverter.Convert(drawing);
        }

        public override DrawingImage ChangeColor(SvgDocument svgDocument, string oldColor, string newColor)
        {
            throw new NotImplementedException();
        }
    }
}
