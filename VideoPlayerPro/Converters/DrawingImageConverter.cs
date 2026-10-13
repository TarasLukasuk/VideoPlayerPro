using System.Windows.Media;

namespace VideoPlayerPro.Converters
{
    internal static class DrawingImageConverter
    {
        public static DrawingImage Convert(DrawingGroup drawingGroup)
        {
            ArgumentNullException.ThrowIfNull(drawingGroup);

            DrawingImage drawingImage = new(drawingGroup);

            if (drawingImage.CanFreeze)
            {
                drawingImage.Freeze();
            }

            return drawingImage;
        }
    }
}
