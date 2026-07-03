namespace ColorPicker.Services.Color;

public static class CieColorTransformService
{
    // sRGB Companding Constants
    private const double SRGB_THRESHOLD = 0.04045;
    private const double SRGB_OFFSET = 0.055;
    private const double SRGB_SCALE_FACTOR = 1.055;
    private const double SRGB_GAMMA_EXPONENT = 2.4;
    private const double SRGB_LINEAR_SLOPE = 12.92;

    // XYZ Matrix Transformation Coefficients
    private const double XR = 0.4124, XG = 0.3576, XB = 0.1805;
    private const double YR = 0.2126, YG = 0.7152, YB = 0.0722;
    private const double ZR = 0.0193, ZG = 0.1192, ZB = 0.9505;

    // D65 Standard Illuminant Reference White Points
    private const double D65_WHITE_X = 95.047;
    private const double D65_WHITE_Y = 100.000;
    private const double D65_WHITE_Z = 108.883;

    // CIELAB Projections Constants
    private const double LAB_CUBIC_THRESHOLD = 0.008856;
    private const double LAB_LINEAR_SLOPE = 7.787;
    private const double LAB_CONSTANT_OFFSET = 16.0 / 116.0;
    private const double LAB_L_SCALE = 116.0;
    private const double LAB_L_OFFSET = 16.0;
    private const double LAB_A_SCALE = 500.0;
    private const double LAB_B_SCALE = 200.0;

    // Calculates the perceptual distance (CIE76 Delta E) between two colors.
    public static double GetDeltaE(Microsoft.Maui.Graphics.Color c1, Microsoft.Maui.Graphics.Color c2) => GetDeltaE(RgbToLab(c1), c2);

    public static double GetDeltaE(LabColor lab1, Microsoft.Maui.Graphics.Color c2) => GetDeltaE(lab1, RgbToLab(c2));

    public static double GetDeltaE(LabColor lab1, LabColor lab2)
    {
        var deltaL = lab1.L - lab2.L;
        var deltaA = lab1.A - lab2.A;
        var deltaB = lab1.B - lab2.B;

        return Math.Sqrt(deltaL * deltaL + deltaA * deltaA + deltaB * deltaB);
    }

    // Standard structural representation of the CIELAB color space
    public record LabColor(double L, double A, double B);

    private static LabColor RgbToLab(Microsoft.Maui.Graphics.Color color)
    {
        var xyz = RgbToXyz(InverseSrgbGamma(color.Red) * 100, InverseSrgbGamma(color.Green) * 100,
            InverseSrgbGamma(color.Blue) * 100);
        return XyzToCielab(xyz.x, xyz.y, xyz.z);
    }

    private static double InverseSrgbGamma(double value) =>
        value > SRGB_THRESHOLD
            ? Math.Pow((value + SRGB_OFFSET) / SRGB_SCALE_FACTOR, SRGB_GAMMA_EXPONENT)
            : value / SRGB_LINEAR_SLOPE;

    public static (double x, double y, double z) RgbToXyz(double r, double g, double b)
    {
        var x = r * XR + g * XG + b * XB;
        var y = r * YR + g * YG + b * YB;
        var z = r * ZR + g * ZG + b * ZB;
        return (x, y, z);
    }

    public static LabColor XyzToCielab(double x, double y, double z)
    {
        x = CielabLinearization(x / D65_WHITE_X);
        y = CielabLinearization(y / D65_WHITE_Y);
        z = CielabLinearization(z / D65_WHITE_Z);

        var l = LAB_L_SCALE * y - LAB_L_OFFSET;
        var gr = LAB_A_SCALE * (x - y);
        var by = LAB_B_SCALE * (y - z);
        
        return new LabColor(l, gr, by);
    }

    private static double CielabLinearization(double value) => 
        value > LAB_CUBIC_THRESHOLD ? Math.Pow(value, 1.0 / 3.0) : LAB_LINEAR_SLOPE * value + LAB_CONSTANT_OFFSET;
}