namespace ColorPicker.Services.Color;

public static class HsvColorHelper
{
    public static (double h, double s, double v) FromRgb(byte r, byte g, byte b) => 
        Microsoft.Maui.Graphics.Color.FromRgb(r, g, b).ToHsv();

    extension(Microsoft.Maui.Graphics.Color color)
    {
        public (double h, double s, double v) ToHsv()
        {
            var hue = color.GetHue() * 360.0; // Normalized to 0-360 range for display/sliders
            var saturation = color.GetHsvSaturation();
            var value = color.GetHsvValue(); // Correctly returns 0.0 - 1.0
            return (hue, saturation, value);
        }
        
        public void ToHsv(out double hue, out double saturation, out double value)
        {
            hue = color.GetHue() * 360.0; // Normalized to 0-360 range for display/sliders
            saturation = color.GetHsvSaturation();
            value = color.GetHsvValue(); // Correctly returns 0.0 - 1.0
        }

        public double GetHsvValue() => 
            Math.Max(color.Red, Math.Max(color.Green, color.Blue));

        public double GetHsvSaturation()
        {
            double max = Math.Max(color.Red, Math.Max(color.Green, color.Blue));
            double min = Math.Min(color.Red, Math.Min(color.Green, color.Blue));
    
            if (max == 0) return 0;
    
            return (max - min) / max;
        }
    }
}