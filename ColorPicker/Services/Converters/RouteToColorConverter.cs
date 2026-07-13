using System.Globalization;

namespace ColorPicker.Services.Converters;

public class RouteToColorConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string activeRoute && parameter is string targetRoute && 
            activeRoute.Equals(targetRoute, StringComparison.InvariantCultureIgnoreCase))
        {
            return GetResource<Microsoft.Maui.Graphics.Color>("Accent");
        }

        return Application.Current?.RequestedTheme == AppTheme.Dark
            ? GetResource<Microsoft.Maui.Graphics.Color>("DarkTextSecondary")
            : GetResource<Microsoft.Maui.Graphics.Color>("LightTextSecondary");
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => Colors.Transparent;
    
    private static T GetResource<T>(string key)
        => Application.Current?.Resources.TryGetValue(key, out var val) == true && val is T t
            ? t
            : default!;
}