using ColorPicker.Resources.Strings;

namespace ColorPicker.Models.Colors;

public class ColorSwatch
{
    private static string DefaultTitle => AppResources.color_default_title;
    public string Name { get; set; } = DefaultTitle;
    public byte Red { get; set; }
    public byte Green { get; set; }
    public byte Blue { get; set; }

    public Color ToColor => Color.FromRgb(Red, Green, Blue);
    public string Hex => $"#{Red:X2}{Green:X2}{Blue:X2}";

    public static ColorSwatch FromColor(Color color, string? name = null) => new()
    {
        Name = name ?? DefaultTitle,
        Red = (byte)Math.Round(color.Red * 255),
        Green = (byte)Math.Round(color.Green * 255),
        Blue = (byte)Math.Round(color.Blue * 255)
    };
    
    public static ColorSwatch FromRgb(double red, double green, double blue, string? name = null) => new()
    {
        Name = name ?? DefaultTitle,
        Red = (byte)red,
        Green = (byte)green,
        Blue = (byte)blue
    };
    
    public bool HexEquals(string other) => Hex.TrimStart('#').Equals(other.TrimStart('#'), StringComparison.InvariantCultureIgnoreCase);
}