using Svg;

namespace VideoPlayerPro.Service
{
    internal abstract class SvgAbstract
    {
        public abstract SvgDocument Load(string svgPath);
        public abstract SvgDocument ChangeColor(SvgDocument svgDocument, string oldColor, string newColor);
    }
}