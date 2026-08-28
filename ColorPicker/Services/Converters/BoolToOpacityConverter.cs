using System.Globalization;

namespace ColorPicker.Services.Converters;

public class BoolToOpacityConverter : IValueConverter
{
    private const double OPAQUE_VALUE = 0.35;
    
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture) => value is true ? 1.0 : OPAQUE_VALUE;
 
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => OPAQUE_VALUE;
}