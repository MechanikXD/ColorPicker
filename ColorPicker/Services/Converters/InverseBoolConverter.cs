using System.Globalization;
using CommunityToolkit.Maui.Converters;

namespace ColorPicker.Services.Converters;

public class InverseBoolConverter : BaseConverterOneWay<bool, bool>
{
    public override bool ConvertFrom(bool value, CultureInfo? culture) => !value;

    public override bool DefaultConvertReturnValue { get; set; } = false;
}