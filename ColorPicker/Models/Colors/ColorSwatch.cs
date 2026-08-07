namespace ColorPicker.Models.Colors;

public class ColorSwatch
{
    private const double EPS = .00001;
    public string Name { get; set; } = "Untitled";
    public byte Red { get; set; }
    public byte Green { get; set; }
    public byte Blue { get; set; }

    public Color ToColor => Color.FromRgb(Red, Green, Blue);
    public string Hex => $"#{Red:X2}{Green:X2}{Blue:X2}";

    public static ColorSwatch FromColor(Color color, string name = "Untitled") => new()
    {
        Name = name,
        Red = (byte)Math.Round(color.Red * 255),
        Green = (byte)Math.Round(color.Green * 255),
        Blue = (byte)Math.Round(color.Blue * 255)
    };
    
    public static ColorSwatch FromRgb(double red, double green, double blue, string name = "Untitled") => new()
    {
        Name = name,
        Red = (byte)red,
        Green = (byte)green,
        Blue = (byte)blue
    };

    public static ColorSwatch FromHsv(double h, double s, double v, string name = "Untitled")
    {
        var c = v * s;
        var x = c * (1 - Math.Abs(h / 60 % 2 - 1));
        var m = v - c;
        double r1, g1, b1;

        switch (h)
        {
            case < 60:
                (r1, g1, b1) = (c, x, 0);
                break;
            case < 120:
                (r1, g1, b1) = (x, c, 0);
                break;
            case < 180:
                (r1, g1, b1) = (0, c, x);
                break;
            case < 240:
                (r1, g1, b1) = (0, x, c);
                break;
            case < 300:
                (r1, g1, b1) = (x, 0, c);
                break;
            default:
                (r1, g1, b1) = (c, 0, x);
                break;
        }

        var r = (byte)Math.Round((r1 + m) * 255);
        var g = (byte)Math.Round((g1 + m) * 255);
        var b = (byte)Math.Round((b1 + m) * 255);

        return new ColorSwatch
        {
            Red = r,
            Green = g,
            Blue = b,
            Name = name
        };
    }

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

    public bool ValueEquals(ColorSwatch other) => 
        Math.Abs(Red - other.Red) < EPS && Math.Abs(Green - other.Green) < EPS && Math.Abs(Blue - other.Blue) < EPS;
    
    public bool HexEquals(string other) => Hex.TrimStart('#').Equals(other.TrimStart('#'), StringComparison.InvariantCultureIgnoreCase);
}