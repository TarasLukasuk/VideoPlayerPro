namespace VideoPlayerPro.Converters;

public class DoubleToPercentConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not double doubleValue)
        {
            return DependencyProperty.UnsetValue;
        }

        return (doubleValue / 100).ToString("P0", culture);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return DependencyProperty.UnsetValue;
    }
}
