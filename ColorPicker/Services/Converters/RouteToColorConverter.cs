using System.Globalization;

namespace ColorPicker.Services.Converters;

public class RouteToColorConverter : IValueConverter
{
    private static readonly Microsoft.Maui.Graphics.Color DefaultReturnColor = Colors.Black;
    
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string activeRoute && parameter is string targetRoute)
        {
            // If they match, fetch the resource color from the application level
            if (activeRoute.Equals(targetRoute, StringComparison.OrdinalIgnoreCase))
            {
                if (Application.Current?.Resources.TryGetValue("Accent", out var accentColor) == true)
                    return (Microsoft.Maui.Graphics.Color)accentColor;
                
                return DefaultReturnColor; // Hardcoded fallback if resource isn't found
            }
        }

        // Otherwise return the secondary text color resource
        if (Application.Current?.Resources.TryGetValue("LightTextSecondary", out var secondaryColor) == true)
            return (Microsoft.Maui.Graphics.Color)secondaryColor;

        return DefaultReturnColor; // fallback
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => string.Empty;
}