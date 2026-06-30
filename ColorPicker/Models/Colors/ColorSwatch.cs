namespace ColorPicker.Models.Colors;

public class ColorSwatch
{
    private const double EPS = .00001;
    public string Name { get; set; } = "Untitled";
    public byte Red { get; set; }
    public byte Green { get; set; }
    public byte Blue { get; set; }

    public Color ToColor() => Color.FromRgb(Red, Green, Blue);
    public string Hex => $"#{Red:X2}{Green:X2}{Blue:X2}";

    public static ColorSwatch FromColor(Color color, string name = "Untitled") => new()
    {
        Name = name,
        Red = (byte)Math.Round(color.Red * 255),
        Green = (byte)Math.Round(color.Green * 255),
        Blue = (byte)Math.Round(color.Blue * 255)
    };

    public (double H, double S, double V) ToHsv()
    {
        double r = Red / 255.0, g = Green / 255.0, b = Blue / 255.0;
        var max = Math.Max(r, Math.Max(g, b));
        var min = Math.Min(r, Math.Min(g, b));
        var delta = max - min;

        var h = .0;
        if (delta > EPS)
        {
            if (Math.Abs(max - r) < EPS) h = 60 * ((g - b) / delta % 6);
            else if (Math.Abs(max - g) < EPS) h = 60 * ((b - r) / delta + 2);
            else h = 60 * ((r - g) / delta + 4);
        }

        if (h < 0) h += 360;
        var s = max <= 0 ? 0 : delta / max;
        return (h, s, max);
    }
}