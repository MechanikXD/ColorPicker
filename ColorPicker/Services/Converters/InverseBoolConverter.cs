using System.Globalization;

namespace ColorPicker.Services.Converters;

public class InverseBoolConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) => value is false;

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => value is true;
}