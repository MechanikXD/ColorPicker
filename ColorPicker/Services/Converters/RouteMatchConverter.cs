using System.Globalization;

namespace ColorPicker.Services.Converters;

public class RouteMatchConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not string activeRoute || parameter is not string targetRoute) return false;
        return activeRoute.Equals(targetRoute, StringComparison.InvariantCultureIgnoreCase);
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => false;
}