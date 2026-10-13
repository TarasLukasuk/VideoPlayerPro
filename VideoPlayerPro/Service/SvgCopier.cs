using System.Windows;
using System.Xml;

namespace VideoPlayerPro.Service
{
    internal static class SvgCopier
    {
        public static T? Copy<T>(T source) where T : XmlDocument
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source), "Source XmlDocument cannot be null.");
            }
            return source.CloneNode(true) as T;
        }
    }
}