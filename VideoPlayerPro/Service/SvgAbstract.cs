using SharpVectors.Dom.Svg;
using System.Windows.Media;

namespace VideoPlayerPro.Service
{
    internal abstract class SvgAbstract
    {
        public abstract DrawingImage Load(string svgName);
        public abstract DrawingImage ChangeColor(SvgDocument svgDocument, string oldColor, string newColor);
    }
}