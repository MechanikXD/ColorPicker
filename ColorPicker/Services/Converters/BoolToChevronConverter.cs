using System.Globalization;

namespace ColorPicker.Services.Converters;

public class BoolToChevronConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool b) return b ? "^" : "v";
        return "-";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => "-";
}