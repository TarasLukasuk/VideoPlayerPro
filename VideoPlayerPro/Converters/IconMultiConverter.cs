using System.Globalization;
using System.Windows;
using System.Windows.Data;
using VideoPlayerPro.Service;

namespace VideoPlayerPro.Converters;

public class IconMultiConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length != 4 || values[0] is not IconsProviderService service || values[1] is not string iconName || values[2] is not string targetChar || values[3] is not string replacementChar || targetChar.Length != 1 || replacementChar.Length != 1)
        {
            return DependencyProperty.UnsetValue;
        }

        return service[iconName, targetChar, replacementChar];
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        return (object[])DependencyProperty.UnsetValue;
    }
}
